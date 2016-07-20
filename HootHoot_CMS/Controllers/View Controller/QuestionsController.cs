using System.Collections.Generic;
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

            return View();
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


            } // End Model.IsValid() IF-Block

            // Perform second pass ModelState check ensure that model state is still valid 
            // NOTE: This is required as it is possible for picture option type to FAIL during the first check, 
            // this has no impact to TEXT option type checks, it will still remain TRUE
            if (ModelState.IsValid)
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

            return View(questions);

        }

        // GET: Questions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Questions questions = questionsGateway.SelectById(id);
            if (questions == null)
            {
                return HttpNotFound();
            }

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

            }

            // Perform second pass ModelState check ensure that model state is still valid 
            // NOTE: This is required as it is possible for picture option type to FAIL during the first check, 
            // this has no impact to TEXT option type checks, it will still remain TRUE
            if (ModelState.IsValid)
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

            ViewBag.correct_option = Constants.customCorrectOption_List(questions.correct_option);
            assignsViewBag_EditQuestion(questions.station_id, questions.question_type, questions.option_type);
            return View(questions);
        }

        // GET: Questions/Delete/5
        public ActionResult Delete(int? id)
        {
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

        private void assignsViewBag_EditQuestion(string stationID, string questionType, string optionType)
        {
            IEnumerable<Stations> stations_Collection = stationGateway.SelectAll();
            List<SelectListItem> stations_List = new List<SelectListItem>();
            string station_id = "";

            foreach(Stations station_Obj in stations_Collection)
            {
                station_id = station_Obj.station_id;
                stations_List.Add(new SelectListItem() { Text = station_Obj.station_name, Value = station_id,
                                  Selected = ( (!string.IsNullOrEmpty(station_id)) && station_id.Equals(stationID) )
                                 });
            }

            IEnumerable<QuestionType> questionTypes_Collection = questionTypeGateway.SelectAll();
            List<SelectListItem> questionType_List = new List<SelectListItem>();
            string qnsType = "";

            foreach (QuestionType qnsType_Obj in questionTypes_Collection)
            {
                qnsType = qnsType_Obj.questiontype;
                questionType_List.Add(new SelectListItem() { Text = qnsType, Value = qnsType,
                                Selected = ((!string.IsNullOrEmpty(qnsType)) && qnsType.Equals(questionType))
                                });
            }

            IEnumerable<OptionType> optionTypes_Collection = optionTypeGateway.SelectAll();
            List<SelectListItem> optionType_List = new List<SelectListItem>();
            string optType = "";

            foreach(OptionType optType_Obj in optionTypes_Collection)
            {
                optType = optType_Obj.optiontype;
                optionType_List.Add(new SelectListItem() { Text = optType, Value =  optType,
                                    Selected = ( (!string.IsNullOrEmpty(optType)) && optType.Equals(optionType) )
                                   });

            }

            ViewBag.station_id = stations_List;
            ViewBag.question_type = questionType_List;
            ViewBag.option_type = optionType_List;


        }

        private void assignsViewBag_FilteringResults()
        {
            //IEnumerable<string> stationNames = questionsGateway.GetStationName_StationID();
            IEnumerable<KeyValuePair<string,string>> stationsKVP = questionsGateway.GetStationHasQuestions();
            List<SelectListItem> stationNamesFilter_List = new List<SelectListItem>();
            stationNamesFilter_List.Add(new SelectListItem() { Text = "===== Filter By Station ====", Value = "NOFILTER" });

            foreach (KeyValuePair<string,string> station in stationsKVP)
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

        private void checkImageFileUpload_Success(byte index, string fileName)
        {
            if (!FileHelper.checkFileExists_Server(fileName))
            {
                //DO NOT LET THE SUBMIT/UPLOAD GO THROUGH, STOP the upload IMMEDIATELY
                setModelState_Error(index, Constants.FILE_UPLOAD_NOT_FOUND);
            }
            //Checks on the file type (extension) to ensure that it is an image
            else if (!FileHelper.checkFileExt_Valid(fileName))
            {
                setModelState_Error(index, Constants.FILE_TYPE_NOT_ACCEPTED);
            }
            //Checks on the width and height dimension of the image file
            else if (!FileHelper.checkImageDimension_Valid(fileName))
            {
                setModelState_Error(index, Constants.PIC_FILE_EXCEEDS_DIMENSION);
            }
        }

        private void checkHasBlobValue_Option(bool[] containsBlob_Val, string[] optionValues_Arr, bool isPict_Option )
        {
            
            for (byte i = 0; i < Constants.OPTIONS_PER_QNS; i++)
            {
                if (containsBlob_Val[i])
                {
                    if (isPict_Option && !FileHelper.checkFileExists_Blob(optionValues_Arr[i]))
                    {
                        setModelState_Error(i, Constants.BLOB_PIC_NOT_FOUND);
                        //passCheck = false;
                    }

                    else if (!isPict_Option)
                    {
                        setModelState_Error(i, Constants.TEXT_OPTION_HAS_BLOB_VALUE);
                        //passCheck = false;
                    } //End Else-If

                } //End of containsBlob_Val[i] IF-Block

                else if(hasInternetAddress(optionValues_Arr[i]) )
                {
                    setModelState_Error(i, (isPict_Option) ? Constants.BLOB_OPTION_HAS_INERNET_ADDR : Constants.TEXT_OPTION_HAS_BLOB_VALUE);
                }

                else if (isPict_Option)
                {
                    checkImageFileUpload_Success(i, optionValues_Arr[i]);
                }

            }//End of For-Loop Block
        }

        private bool hasInternetAddress(string optionValue)
        {
            optionValue = optionValue.ToUpper();
            for(byte i=0;i<Constants.INTERNET_ADDRESS_PATTERN.Length;i++)
            {
                if(optionValue.Contains(Constants.INTERNET_ADDRESS_PATTERN[i]))
                {
                    return true;
                }
            }
            return false;
        }

        private void setModelState_Error(byte index, string errorMsg)
        {
            ModelState.AddModelError(
                        ModelState.Keys.Single(field => field == "option_" + (index + 1)),
                            errorMsg);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
