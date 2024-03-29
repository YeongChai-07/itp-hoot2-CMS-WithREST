﻿using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;

using HootHoot_CMS.DAL;
using HootHoot_CMS.Models;
using HootHoot_CMS.Blobs;

namespace HootHoot_CMS.Controllers.View_Controller
{
    public class QuestionsController : Controller
    {
        private QuestionsDataGateway questionsGateway = new QuestionsDataGateway();
        private QuestionTypeDataGateway questionTypeGateway = new QuestionTypeDataGateway();
        private OptionTypeDataGateway optionTypeGateway = new OptionTypeDataGateway();
        private StationDataGateway stationGateway = new StationDataGateway();

        // GET: Questions
        public ActionResult Index()
        {
            //Check whether has the user logon to CMS, if the user
            // has not logon to CMS, redirect the user to the login page
            if (Session["logon_user"] == null)
            { return RedirectToAction("Login", "Accounts", new { returnUrl = Request.RawUrl }); }

            var questions = questionsGateway.SelectAll_Joint();

            assignsViewBag_FilteringResults();

            return View(questions.ToList());
        }

        [HttpPost]
        public ActionResult Index(FilterQuestionsViewModel filterQuestions)
        {
            var questions = questionsGateway.SelectAll_Joint();

            assignsViewBag_FilteringResults();

            //No need to check whether FilterQuestionsViewModel object is null, as
            //it will NEVER be null.
            string station_Filter = filterQuestions.filter_station;
            string questionType_Filter = filterQuestions.filter_questiontype;
            string optionType_Filter = filterQuestions.filter_optiontype;

            if (station_Filter != null && !station_Filter.Equals("NOFILTER"))
            {
                //Now the value of station_Filter respresents the value of the station_id
                //This is due to each item in filterStation select list holds the value of station_id
                questions = questions.Where(qns => qns.station_id == station_Filter);
            }

            if (questionType_Filter != null && !questionType_Filter.Equals("NOFILTER"))
            {
                questions = questions.Where(qns => qns.question_type == questionType_Filter);
            }

            if (optionType_Filter != null && !optionType_Filter.Equals("NOFILTER"))
            {
                questions = questions.Where(qns => qns.option_type == optionType_Filter);
            }

            return View(questions.ToList());
        }
        // GET: Questions/Details/5
        public ActionResult Details(int? id)
        {
            //Check whether has the user logon to CMS, if the user
            // has not logon to CMS, redirect the user to the login page
            if (Session["logon_user"] == null)
            { return RedirectToAction("Login", "Accounts", new { returnUrl = Request.RawUrl }); }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Questions questions = questionsGateway.SelectById(id);
            if (questions == null)
            {
                return HttpNotFound();
            }

            return View(questions);
        }

        // GET: Questions/Create
        public ActionResult Create()
        {
            //Check whether has the user logon to CMS, if the user
            // has not logon to CMS, redirect the user to the login page
            if (Session["logon_user"] == null)
            { return RedirectToAction("Login", "Accounts", new { returnUrl = Request.RawUrl }); }

            setupImageOptions_ViewBag();
            Questions question = TempData["toSubmit_Qns"] as Questions;
            KeyValuePair<string, string>[] imageUpload_Validate = TempData["imageUpload_Validate"] as KeyValuePair<string, string>[];
            if (question !=null)
            {
                assignsViewBag_EditQuestion(question.station_id, question.question_type, question.option_type);

                if(imageUpload_Validate != null)
                {
                    for (byte i = 0; i < imageUpload_Validate.Length; i++)
                    {
                        if (!imageUpload_Validate[i].Value.Equals("PASSED"))
                        {
                            setModelState_Error(i, imageUpload_Validate[i].Value);
                        }

                    }
                }

                if (question.option_type.Equals(Constants.QNS_IMAGE_OPTION_TYPE))
                {
                    byte optionNo = 0;
                    for (byte i = 0; i < Constants.OPTIONS_PER_QNS; i++)
                    {
                        optionNo = (byte)(i + 1);
                        assignImageOptionsValue_ViewBag(optionNo, question.getsOption_Value(optionNo),
                            Constants.OPT_UPLOAD_IMAGE_STATE);
                    }
                }
                
                return View(question);
            }
            else
            {
                assignsViewBag_CreateQuestion();
                return View();
            }
            
            //return View();
        }

        // POST: Questions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Questions questions)
        {

            bool modelState_FirstPass = ModelState.IsValid; // First-Pass check of the ModelState Valid property
            bool is_ImageOptionType = false; // Initialized to false to avoid complications
            string[] listOfFiles = null;

            if (modelState_FirstPass)
            {
                is_ImageOptionType = questions.option_type == Constants.QNS_IMAGE_OPTION_TYPE;
                if (is_ImageOptionType)
                {
                    //Checks whether the specified file for each option exists in server
                    listOfFiles = new string[] { questions.option_1, questions.option_2, questions.option_3, questions.option_4 };

                    //iterate each item in listOfFiles to check whether the file exists in web server
                    for (byte i = 0; i < listOfFiles.Length; i++)
                    {
                        checkImageFileUpload_Success(i, listOfFiles[i]);

                    } // End FOR-Loop

                } // End question.option_type IF-Block

                // Do nothing if question.option_type != "IMAGE"

                if(questions.question_has_hint.Equals("NO"))
                {
                    questions.hint = "-NA-";
                }


            } // End Model.IsValid() IF-Block

            // Perform second pass ModelState check ensure that model state is still valid 
            // NOTE: This is required as it is possible for picture option type to FAIL during the first check, 
            // this has no impact to TEXT option type checks, it will still remain TRUE
            if (ModelState.IsValid)
            {
                TempData["toSubmit_Qns"] = questions;
                return RedirectToAction("CreateConfirm");
            }

            assignsViewBag_CreateQuestion();
            setupImageOptions_ViewBag();

            if(is_ImageOptionType)
            {
                byte optionNo = 0;
                for(byte i=0; i<Constants.OPTIONS_PER_QNS;i++)
                {
                    optionNo = (byte)(i + 1);
                    assignImageOptionsValue_ViewBag(optionNo, questions.getsOption_Value(optionNo), Constants.OPT_UPLOAD_IMAGE_STATE);
                }
            }

            return View(questions);

        }

        public ActionResult CreateConfirm()
        {
            Questions question = TempData["toSubmit_Qns"] as Questions;

            if(question !=null)
            {
                QuestionsViewModel question_options = new QuestionsViewModel();
                if(question.option_type.Equals(Constants.QNS_IMAGE_OPTION_TYPE))
                {
                    
                    question_options.option_1 = "<img src=\"" + Constants.IMG_PIC_UPLOAD_SRC + question.option_1
                        + "\" alt=\"Option 1 Image\" width=\"100\" height=\"100\" />";
                    question_options.option_2 = "<img src=\"" + Constants.IMG_PIC_UPLOAD_SRC + question.option_2
                        + "\" alt=\"Option 2 Image\" width=\"100\" height=\"100\" />";
                    question_options.option_3 = "<img src=\"" + Constants.IMG_PIC_UPLOAD_SRC + question.option_3
                        + "\" alt=\"Option 3 Image\" width=\"100\" height=\"100\" />";
                    question_options.option_4 = "<img src=\"" + Constants.IMG_PIC_UPLOAD_SRC + question.option_4
                        + "\" alt=\"Option 4 Image\" width=\"100\" height=\"100\" />";
                }
                else
                {
                    question_options.option_1 = question.option_1;
                    question_options.option_2 = question.option_2;
                    question_options.option_3 = question.option_3;
                    question_options.option_4 = question.option_4;
                }

                ViewBag.question_options = question_options;
                ViewBag.station_name = stationGateway.GetStationName_ByStationID(question.station_id);
                return View(question);
            }

            return RedirectToAction("Create");
            
        }

        [HttpPost, ActionName("CreateConfirm")]
        public ActionResult CreateConfirmed(Questions questions)
        {
            bool is_ImageOptionType = questions.option_type == Constants.QNS_IMAGE_OPTION_TYPE;
            KeyValuePair<string, string>[] imageValidation_Arr = new KeyValuePair<string, string>[Constants.OPTIONS_PER_QNS];
            string[] listOfFiles = null;
            bool modelState_Valid = true;

            if (is_ImageOptionType)
            {
                //Checks whether the specified file for each option exists in server
                listOfFiles = new string[] { questions.option_1, questions.option_2, questions.option_3, questions.option_4 };

                //iterate each item in listOfFiles to check whether the file exists in web server
                for (byte i = 0; i < listOfFiles.Length; i++)
                {
                    imageValidation_Arr[i] = checkImageFileUpload_Success(i, listOfFiles[i]);

                    if(!modelState_Valid)
                    {
                        continue;
                    }

                    modelState_Valid = imageValidation_Arr[i].Value.Equals("PASSED");

                } // End FOR-Loop

            } // End question.option_type IF-Block

            // Do nothing if question.option_type != "IMAGE"

            // Perform second pass ModelState check ensure that model state is still valid 
            // NOTE: This is required as it is possible for picture option type to FAIL during the first check, 
            // this has no impact to TEXT option type checks, it will still remain TRUE
            if (modelState_Valid)
            {
                if (is_ImageOptionType)
                {
                    //Uploads and gets the blob URI string to each image option
                    BlobManager picBlob = new BlobManager();
                    questions.option_1 = picBlob.uploadPictureToBlob(listOfFiles[0]);
                    questions.option_2 = picBlob.uploadPictureToBlob(listOfFiles[1]);
                    questions.option_3 = picBlob.uploadPictureToBlob(listOfFiles[2]);
                    questions.option_4 = picBlob.uploadPictureToBlob(listOfFiles[3]);
                }

                questionsGateway.Insert(questions);

                //Assume : All image options files were uploaded to Azure Blob SUCCESSFULLY.
                FileHelper.deletesALLUploadFiles(); //Deletes all pictures from the server upload folder

                return RedirectToAction("Index");
            }

            TempData["toSubmit_Qns"] = questions;
            TempData["imageUpload_Validate"] = imageValidation_Arr;

            return RedirectToAction("Create");
        }

        [HttpPost]
        public ActionResult CreateBack(Questions question)
        {
            TempData["toSubmit_Qns"] = question;

            return RedirectToAction("Create");
        }

        // GET: Questions/Edit/5
        public ActionResult Edit(int? id)
        {
            //Check whether has the user logon to CMS, if the user
            // has not logon to CMS, redirect the user to the login page
            if (Session["logon_user"] == null)
            { return RedirectToAction("Login", "Accounts", new { returnUrl = Request.RawUrl }); }

            setupImageOptions_ViewBag();
            Questions received_Questions = TempData["toSubmit_Qns"] as Questions;
            Questions questions = null;
            KeyValuePair<string, string>[] validationErrors_KVP = TempData["editQns_Validate"] as KeyValuePair<string,string>[];

            if (received_Questions == null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                questions = questionsGateway.SelectById(id);

                if (questions == null)
                {
                    return HttpNotFound();
                }

            }
            else
            {
                questions = received_Questions;

                if (validationErrors_KVP != null)
                {
                    for (byte i = 0; i < validationErrors_KVP.Length; i++)
                    {
                        if (!validationErrors_KVP[i].Value.Equals("PASSED"))
                        {
                            setModelState_Error(i, validationErrors_KVP[i].Value);
                        }

                    }
                } // End inner if
            }

            if (questions.option_type.Equals(Constants.QNS_IMAGE_OPTION_TYPE))
            {
                byte optionNo = 0;
                string optionValue = "";
                for (byte i = 0; i < Constants.OPTIONS_PER_QNS; i++)
                {
                    optionNo = (byte)(i + 1);
                    optionValue = questions.getsOption_Value(optionNo);
                    assignImageOptionsValue_ViewBag(optionNo, optionValue,
                        optionValue.Contains(Constants.AZURE_BLOB_STORAGE_FOLDER));
                }

            }// End inner if

            ViewBag.correct_option = Constants.customCorrectOption_List(questions.correct_option);
            assignsViewBag_EditQuestion(questions.station_id, questions.question_type, questions.option_type);

            return View(questions);
        }

        // POST: Questions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Questions questions)
        {
            bool modelState_FirstPass = ModelState.IsValid; // First-Pass check of the ModelState Valid property
            bool is_ImageOptionType = false; // Initialized to false to avoid complications
            string[] optionValues_Arr = null;
            bool[] containsBlob_Val = null;

            if (modelState_FirstPass)
            {
                is_ImageOptionType = questions.option_type == Constants.QNS_IMAGE_OPTION_TYPE;
                optionValues_Arr = new string[] { questions.option_1, questions.option_2, questions.option_3, questions.option_4 };
                containsBlob_Val = new bool[]
                {
                    //Check and initialize each of the option field whether it has blob storage address format
                    optionValues_Arr[0].Contains(Constants.AZURE_BLOB_STORAGE_FOLDER),
                    optionValues_Arr[1].Contains(Constants.AZURE_BLOB_STORAGE_FOLDER),
                    optionValues_Arr[2].Contains(Constants.AZURE_BLOB_STORAGE_FOLDER),
                    optionValues_Arr[3].Contains(Constants.AZURE_BLOB_STORAGE_FOLDER)
                };

                checkHasBlobValue_Option(containsBlob_Val, optionValues_Arr, is_ImageOptionType);

                if (questions.question_has_hint.Equals("NO"))
                {
                    questions.hint = "-NA-";
                }

            }

            // Perform second pass ModelState check ensure that model state is still valid 
            // NOTE: This is required as it is possible for picture option type to FAIL during the first check, 
            // this has no impact to TEXT option type checks, it will still remain TRUE
            if (ModelState.IsValid)
            {
                TempData["toSubmit_Qns"] = questions;
                return RedirectToAction("EditConfirm");
            }

            setupImageOptions_ViewBag();
            if(is_ImageOptionType)
            {
                byte optionNo = 0;
                for (byte i = 0; i < Constants.OPTIONS_PER_QNS; i++)
                {
                    optionNo = (byte)(i + 1);
                    assignImageOptionsValue_ViewBag(optionNo, questions.getsOption_Value(optionNo), 
                        (containsBlob_Val[i]));
                }
            }

            ViewBag.correct_option = Constants.customCorrectOption_List(questions.correct_option);
            assignsViewBag_EditQuestion(questions.station_id, questions.question_type, questions.option_type);
            return View(questions);
        }


        public ActionResult EditConfirm()
        {
            Questions question = TempData["toSubmit_Qns"] as Questions;

            if (question != null)
            {
                QuestionsViewModel question_options = new QuestionsViewModel();
                if (question.option_type.Equals(Constants.QNS_IMAGE_OPTION_TYPE))
                {
                    string[] imgOptions_Arr = new string[]
                    { question.option_1, question.option_2, question.option_3, question.option_4};

                    for(byte i=0;i<Constants.OPTIONS_PER_QNS;i++)
                    {
                        if(!imgOptions_Arr[i].Contains(Constants.AZURE_BLOB_STORAGE_FOLDER))
                        {
                            imgOptions_Arr[i] = Constants.IMG_PIC_UPLOAD_SRC + imgOptions_Arr[i];
                        }
                    }
                    
                    question_options.option_1 = "<img src=\"" + imgOptions_Arr[0]
                        + "\" alt=\"Option 1 Image\" width=\"100\" height=\"100\" />";
                    question_options.option_2 = "<img src=\"" + imgOptions_Arr[1]
                        + "\" alt=\"Option 2 Image\" width=\"100\" height=\"100\" />";
                    question_options.option_3 = "<img src=\"" + imgOptions_Arr[2]
                        + "\" alt=\"Option 3 Image\" width=\"100\" height=\"100\" />";
                    question_options.option_4 = "<img src=\"" + imgOptions_Arr[3]
                        + "\" alt=\"Option 4 Image\" width=\"100\" height=\"100\" />";
                }
                else
                {
                    question_options.option_1 = question.option_1;
                    question_options.option_2 = question.option_2;
                    question_options.option_3 = question.option_3;
                    question_options.option_4 = question.option_4;
                }

                ViewBag.question_options = question_options;
                ViewBag.station_name = stationGateway.GetStationName_ByStationID(question.station_id);
                return View(question);
            }

            return RedirectToAction("Edit");

        }

        [HttpPost, ActionName("EditConfirm")]
        public ActionResult EditConfirmed(Questions questions)
        {
            bool is_ImageOptionType = questions.option_type == Constants.QNS_IMAGE_OPTION_TYPE;
            KeyValuePair<string, string>[] validationErrors_KVP = new KeyValuePair<string, string>[Constants.OPTIONS_PER_QNS];
            bool modelState_Valid = true;

            string[] optionValues_Arr = null;
            bool[] containsBlob_Val = null;

            if (is_ImageOptionType)
            {
                optionValues_Arr = new string[] { questions.option_1, questions.option_2, questions.option_3, questions.option_4 };
                containsBlob_Val = new bool[]
                {
                    //Check and initialize each of the option field whether it has blob storage address format
                    optionValues_Arr[0].Contains(Constants.AZURE_BLOB_STORAGE_FOLDER),
                    optionValues_Arr[1].Contains(Constants.AZURE_BLOB_STORAGE_FOLDER),
                    optionValues_Arr[2].Contains(Constants.AZURE_BLOB_STORAGE_FOLDER),
                    optionValues_Arr[3].Contains(Constants.AZURE_BLOB_STORAGE_FOLDER)
                };

                validationErrors_KVP = checkHasBlobValue_Option
                    (containsBlob_Val, optionValues_Arr, is_ImageOptionType);

                for (byte i = 0; i < Constants.OPTIONS_PER_QNS; i++)
                {
                    modelState_Valid = validationErrors_KVP[i].Value.Equals("PASSED");
                    if (!modelState_Valid)
                    {
                        break;
                    }

                } // End FOR-Loop

            }

            // Perform second pass ModelState check ensure that model state is still valid 
            // NOTE: This is required as it is possible for picture option type to FAIL during the first check, 
            // this has no impact to TEXT option type checks, it will still remain TRUE
            if (modelState_Valid)
            {
                if (is_ImageOptionType)
                {
                    BlobManager picBlob = new BlobManager();
                    questions.option_1 = !containsBlob_Val[0] ? picBlob.uploadPictureToBlob(optionValues_Arr[0]) : optionValues_Arr[0];
                    questions.option_2 = !containsBlob_Val[1] ? picBlob.uploadPictureToBlob(optionValues_Arr[1]) : optionValues_Arr[1];
                    questions.option_3 = !containsBlob_Val[2] ? picBlob.uploadPictureToBlob(optionValues_Arr[2]) : optionValues_Arr[2];
                    questions.option_4 = !containsBlob_Val[3] ? picBlob.uploadPictureToBlob(optionValues_Arr[3]) : optionValues_Arr[3];
                }

                questionsGateway.Update(questions);

                //Assume : All image options files were uploaded to Azure Blob SUCCESSFULLY.
                FileHelper.deletesALLUploadFiles(); //Deletes all pictures from the server upload folder

                return RedirectToAction("Index");
            }

            TempData["toSubmit_Qns"] = questions;
            TempData["editQns_Validate"] = validationErrors_KVP;

            return RedirectToAction("Edit");

        }

        [HttpPost]
        public ActionResult EditBack(Questions question)
        {
            TempData["toSubmit_Qns"] = question;

            return RedirectToAction("Edit");
        }

        // GET: Questions/Delete/5
        public ActionResult Delete(int? id)
        {
            //Check whether has the user logon to CMS, if the user
            // has not logon to CMS, redirect the user to the login page
            if (Session["logon_user"] == null)
            { return RedirectToAction("Login", "Accounts", new { returnUrl = Request.RawUrl }); }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Questions questions = questionsGateway.SelectById(id);
            if (questions == null)
            {
                return HttpNotFound();
            }

            return View(questions);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Questions questions = questionsGateway.SelectById(id);
            questionsGateway.Delete(questions);
            return RedirectToAction("Index");
        }


        private void assignsViewBag_CreateQuestion()
        {
            IEnumerable<Stations> stationsForQns_Collection = stationGateway.GetStations_ByStationType(Constants.STATION_TYPE_FOR_QNS);
            List<SelectListItem> stations_List = new List<SelectListItem>();

            foreach (Stations station_Obj in stationsForQns_Collection)
            {
                stations_List.Add(new SelectListItem()
                {
                    Text = station_Obj.station_name,
                    Value = station_Obj.station_id
                });
            }

            IEnumerable<QuestionType> questionTypes_Collection = questionTypeGateway.SelectAll();
            List<SelectListItem> questionType_List = new List<SelectListItem>();

            foreach (QuestionType qnsType_Obj in questionTypes_Collection)
            {
                questionType_List.Add(new SelectListItem()
                {
                    Text = qnsType_Obj.questiontype,
                    Value = qnsType_Obj.questiontype
                });
            }

            IEnumerable<OptionType> optionTypes_Collection = optionTypeGateway.SelectAll();
            List<SelectListItem> optionType_List = new List<SelectListItem>();

            foreach (OptionType optType_Obj in optionTypes_Collection)
            {
                optionType_List.Add(new SelectListItem()
                {
                    Text = optType_Obj.optiontype,
                    Value = optType_Obj.optiontype
                });

            }

            ViewBag.station_id = stations_List;
            ViewBag.question_type = questionType_List;
            ViewBag.option_type = optionType_List;


        }

        private void assignsViewBag_EditQuestion(string stationID, string questionType, string optionType)
        {
            IEnumerable<Stations> stationsForQns_Collection = stationGateway.GetStations_ByStationType(Constants.STATION_TYPE_FOR_QNS);
            List<SelectListItem> stations_List = new List<SelectListItem>();
            string station_id = "";

            foreach (Stations station_Obj in stationsForQns_Collection)
            {
                station_id = station_Obj.station_id;
                stations_List.Add(new SelectListItem()
                {
                    Text = station_Obj.station_name,
                    Value = station_id,
                    Selected = ((!string.IsNullOrEmpty(station_id)) && station_id.Equals(stationID))
                });
            }

            IEnumerable<QuestionType> questionTypes_Collection = questionTypeGateway.SelectAll();
            List<SelectListItem> questionType_List = new List<SelectListItem>();
            string qnsType = "";

            foreach (QuestionType qnsType_Obj in questionTypes_Collection)
            {
                qnsType = qnsType_Obj.questiontype;
                questionType_List.Add(new SelectListItem()
                {
                    Text = qnsType,
                    Value = qnsType,
                    Selected = ((!string.IsNullOrEmpty(qnsType)) && qnsType.Equals(questionType))
                });
            }

            IEnumerable<OptionType> optionTypes_Collection = optionTypeGateway.SelectAll();
            List<SelectListItem> optionType_List = new List<SelectListItem>();
            string optType = "";

            foreach (OptionType optType_Obj in optionTypes_Collection)
            {
                optType = optType_Obj.optiontype;
                optionType_List.Add(new SelectListItem()
                {
                    Text = optType,
                    Value = optType,
                    Selected = ((!string.IsNullOrEmpty(optType)) && optType.Equals(optionType))
                });

            }

            ViewBag.station_id = stations_List;
            ViewBag.question_type = questionType_List;
            ViewBag.option_type = optionType_List;


        }

        private void assignsViewBag_FilteringResults()
        {
            //IEnumerable<string> stationNames = questionsGateway.GetStationName_StationID();
            IEnumerable<KeyValuePair<string, string>> stationsKVP = questionsGateway.GetStationHasQuestions();
            List<SelectListItem> stationNamesFilter_List = new List<SelectListItem>();
            stationNamesFilter_List.Add(new SelectListItem() { Text = "===== Filter By Station ====", Value = "NOFILTER" });

            foreach (KeyValuePair<string, string> station in stationsKVP)
            {
                stationNamesFilter_List.Add(new SelectListItem() { Text = station.Value, Value = station.Key });
            }

            List<SelectListItem> questionTypeFilter_List = new List<SelectListItem>();
            questionTypeFilter_List.Add(new SelectListItem() { Text = "===== Filter By Question Type ====", Value = "NOFILTER" });
            foreach (string questionType in (questionTypeGateway.GetAllQuestionTypes()))
            {
                questionTypeFilter_List.Add(new SelectListItem() { Text = questionType, Value = questionType });
            }

            List<SelectListItem> optionTypeFilter_List = new List<SelectListItem>();
            optionTypeFilter_List.Add(new SelectListItem() { Text = "===== Filter By Option Type ====", Value = "NOFILTER" });
            foreach (string optionType in (optionTypeGateway.GetAllOptionTypes()))
            {
                optionTypeFilter_List.Add(new SelectListItem() { Text = optionType, Value = optionType });
            }

            //Attaches the Filter list to the respective ViewBags
            ViewBag.filter_station = stationNamesFilter_List;
            ViewBag.filter_questiontype = questionTypeFilter_List;
            ViewBag.filter_optiontype = optionTypeFilter_List;
           
        }

        private KeyValuePair<string, string> checkImageFileUpload_Success(byte index, string fileName)
        {
            if (!FileHelper.checkFileExists_Server(fileName))
            {
                //DO NOT LET THE SUBMIT/UPLOAD GO THROUGH, STOP the upload IMMEDIATELY
                setModelState_Error(index, Constants.FILE_UPLOAD_NOT_FOUND);
                return new KeyValuePair<string, string>(Constants.QNS_OPTIONS_MODEL_KEYS[index], 
                    Constants.FILE_UPLOAD_NOT_FOUND);
            }
            //Checks on the file type (extension) to ensure that it is an image
            if (!FileHelper.checkFileExt_Valid(fileName))
            {
                setModelState_Error(index, Constants.FILE_TYPE_NOT_ACCEPTED);
                return new KeyValuePair<string, string>(Constants.QNS_OPTIONS_MODEL_KEYS[index], 
                    Constants.FILE_TYPE_NOT_ACCEPTED);
            }
            //Checks on the width and height dimension of the image file
            if (!FileHelper.checkImageDimension_Valid(fileName))
            {
                setModelState_Error(index, Constants.PIC_FILE_EXCEEDS_DIMENSION);
                return new KeyValuePair<string, string>(Constants.QNS_OPTIONS_MODEL_KEYS[index],
                    Constants.PIC_FILE_EXCEEDS_DIMENSION);
            }

            //KeyValuePair<string, string> testKVP = new KeyValuePair<string, string>();

            return new KeyValuePair<string, string>(Constants.QNS_OPTIONS_MODEL_KEYS[index], "PASSED");
        }

        private KeyValuePair<string, string>[] checkHasBlobValue_Option(bool[] containsBlob_Val, string[] optionValues_Arr, bool isPict_Option)
        {
            KeyValuePair<string, string>[] validationErrors_KVP = new KeyValuePair<string, string>[Constants.OPTIONS_PER_QNS];
            for (byte i = 0; i < Constants.OPTIONS_PER_QNS; i++)
            {
                if (containsBlob_Val[i])
                {
                    if (isPict_Option && !FileHelper.checkFileExists_Blob(optionValues_Arr[i]))
                    {
                        setModelState_Error(i, Constants.BLOB_PIC_NOT_FOUND);
                        validationErrors_KVP[i] = new KeyValuePair<string, string>
                            (Constants.QNS_OPTIONS_MODEL_KEYS[i], Constants.BLOB_PIC_NOT_FOUND);
                        //passCheck = false;
                    }

                    else if (!isPict_Option)
                    {
                        setModelState_Error(i, Constants.TEXT_OPTION_HAS_BLOB_VALUE);
                        validationErrors_KVP[i] = new KeyValuePair<string, string>
                            (Constants.QNS_OPTIONS_MODEL_KEYS[i], Constants.TEXT_OPTION_HAS_BLOB_VALUE);
                        //passCheck = false;
                    } //End Else-If
                    else
                    {
                        validationErrors_KVP[i] = new KeyValuePair<string, string>(Constants.QNS_OPTIONS_MODEL_KEYS[i], "PASSED");
                    }

                } //End of containsBlob_Val[i] IF-Block

                else if (hasInternetAddress(optionValues_Arr[i]))
                {
                    setModelState_Error(i, (isPict_Option) ? Constants.BLOB_OPTION_HAS_INERNET_ADDR : Constants.TEXT_OPTION_HAS_BLOB_VALUE);
                    validationErrors_KVP[i] = new KeyValuePair<string, string>
                            (Constants.QNS_OPTIONS_MODEL_KEYS[i], 
                                (isPict_Option) ? Constants.BLOB_OPTION_HAS_INERNET_ADDR : Constants.TEXT_OPTION_HAS_BLOB_VALUE);
                }

                else if (isPict_Option)
                {
                    validationErrors_KVP[i] = checkImageFileUpload_Success(i, optionValues_Arr[i]);
                }

            }//End of For-Loop Block

            return validationErrors_KVP;
        }

        private bool hasInternetAddress(string optionValue)
        {
            optionValue = optionValue.ToUpper();
            for (byte i = 0; i < Constants.INTERNET_ADDRESS_PATTERN.Length; i++)
            {
                if (optionValue.Contains(Constants.INTERNET_ADDRESS_PATTERN[i]))
                {
                    return true;
                }
            }
            return false;
        }

        private void setModelState_Error(byte index, string errorMsg)
        {
            ModelState.AddModelError(Constants.QNS_OPTIONS_MODEL_KEYS[index], errorMsg);
        }

        private void setupImageOptions_ViewBag()
        {
            QuestionsViewModel question_options = new QuestionsViewModel()
            {
                option_1 = Constants.IMG_NO_PREVIEW_SRC,
                option_2 = Constants.IMG_NO_PREVIEW_SRC,
                option_3 = Constants.IMG_NO_PREVIEW_SRC,
                option_4 = Constants.IMG_NO_PREVIEW_SRC
            };

            ViewBag.question_options = question_options;
        }

        private void assignImageOptionsValue_ViewBag(byte optionNo, string optionValue, bool isExisting_Blob)
        {
            if(! isExisting_Blob)
            {
                ViewBag.question_options.assignsOption_Value(optionNo, Constants.IMG_PIC_UPLOAD_SRC + optionValue);
                return;
            }
            ViewBag.question_options.assignsOption_Value(optionNo, optionValue);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
