$(document).ready(
            function () {
                var prevSelectedVal = undefined;
                // Add events
                $("[name='option1_img']").on('change', prepareUpload);
                $("[name='option2_img']").on('change', prepareUpload);
                $("[name='option3_img']").on('change', prepareUpload);
                $("[name='option4_img']").on('change', prepareUpload);

                $("#addImg_Option1").click(function () {
                    //Trigger the first image option input to open the select file dialog
                    $("[name='option1_img']").click();

                });

                $("#addImg_Option2").click(function () {
                    //Trigger the second image option input to open the select file dialog
                    $("[name='option2_img']").click();

                });

                $("#addImg_Option3").click(function () {
                    //Trigger the third image option input to open the select file dialog
                    $("[name='option3_img']").click();

                });

                $("#addImg_Option4").click(function () {
                    //Trigger the fourth image option input to open the select file dialog
                    $("[name='option4_img']").click();

                });

                $("#option_type").on("focusin", function (event) {
                    prevSelectedVal = $("#option_type option:selected").text();
                    //event.stopPropagation();
                //alert("The old value is : " + test);
                    $("#option_type").blur();
                });

                $("#option_type").change(function () {
                    //var selectedOptionType = $("#option_type option:selected").text();
                        //alert("Selected Value: " + selectedOptionType);
                    checkSelectedOptionType();

                });//End optionType change event

                $("#correct_option").change(function () {
                    //Get its selected value
                    var selectedCorrectOption = $("#correct_option option:selected").text();
                    var imageToShow = "";
                    if(selectedCorrectOption == "Option 1")
                    {
                        imageToShow = $("#imgOption1_Preview").attr("src");
                    }
                    else if (selectedCorrectOption == "Option 2")
                    {
                        imageToShow = $("#imgOption2_Preview").attr("src");
                    }
                    else if(selectedCorrectOption == "Option 3")
                    {
                        imageToShow = $("#imgOption3_Preview").attr("src");
                    }
                    else if(selectedCorrectOption == "Option 4")
                    {
                        imageToShow = $("#imgOption4_Preview").attr("src");
                    }

                    //Now let's assign the correct option image preview src
                    $("#imgCorrectOpt_Preview").attr("src", imageToShow);

                    /*$("[name='question_has_hint']").click(function () {
                        alert('Selected hint: ' + $("[name='question_has_hint'] :checked").val());
                    });*/
                    /*if(!$("[name='question_has_hint']:checked").val() == [])
                    {
                        alert('Selected Hint: ' + $("[name='question_has_hint']:checked").val());
                    }*/

                });


                //Hides ALL file input controls BY DEFAULT
                $("[name='option1_img']").hide();
                $("[name='option2_img']").hide();
                $("[name='option3_img']").hide();
                $("[name='option4_img']").hide();

                //Hides ALL img element for all options preview BY DEFAULT
                $("#imgOption1_Preview").hide();
                $("#imgOption2_Preview").hide();
                $("#imgOption3_Preview").hide();
                $("#imgOption4_Preview").hide();
                $("#imgCorrectOpt_Preview").hide();

                checkSelectedOptionType();

                function resetsImagePreview()
                {
                    $("#imgOption1_Preview").attr("src", "../Images/NoPreview.png");
                    $("#imgOption2_Preview").attr("src", "../Images/NoPreview.png");
                    $("#imgOption3_Preview").attr("src", "../Images/NoPreview.png");
                    $("#imgOption4_Preview").attr("src", "../Images/NoPreview.png");
                    $("#imgCorrectOpt_Preview").attr("src", "../Images/NoPreview.png");

                }

                function checkSelectedOptionType() {                 
                    var currOptionType = $("#option_type option:selected").text();

                    if (typeof prevSelectedVal!== 'undefined')
                    {
                        if (!(currOptionType == prevSelectedVal) && confirm("Are you sure you want to continue?\n You will lose unsaved changes"))
                        {
                            //This also represents that the user confirms and opt to switch option type
                            //We will then empty ALL options textfield
                            $("#option_1").val("");
                            $("#option_2").val("");
                            $("#option_3").val("");
                            $("#option_4").val("");
                        }

                        else
                        {
                            $("[value='" + prevSelectedVal + "']").prop("selected", true);
                            return;
                        }

                    }
                

                    if ($("#option_type option:selected").text() == "IMAGE") {

                            //Disable (Read Only) all the options textfield
                        disableOptions_TextFields(true);

                            //Enables and shows all the add Image buttons
                        disableAddImage_Button(false);

                        return;
                    } //End of selectedOptionType IF Block


                    //Enables and shows all the add Image buttons
                    disableAddImage_Button(true);

                    //Enable all the options textfield
                    disableOptions_TextFields(false);


                } //End of checkSelectedOptionType function

                function disableOptions_TextFields(disableCond) {
                    //Hide all the options textfield
                    if (disableCond)
                    {
                        $("#option_1").hide();
                        $("#option_2").hide();
                        $("#option_3").hide();
                        $("#option_4").hide();
                    }
                    else
                    {
                        $("#option_1").show();
                        $("#option_2").show();
                        $("#option_3").show();
                        $("#option_4").show();
                    }
                    

                    //Disable all the options textfield
                    $("#option_1").prop("readonly", disableCond);
                    $("#option_2").prop("readonly", disableCond);
                    $("#option_3").prop("readonly", disableCond);
                    $("#option_4").prop("readonly", disableCond);

                    
                }

                function disableAddImage_Button(disableCond) {
                    $("#addImg_Option1").prop("disabled", disableCond);
                    $("#addImg_Option2").prop("disabled", disableCond);
                    $("#addImg_Option3").prop("disabled", disableCond);
                    $("#addImg_Option4").prop("disabled", disableCond);

                    if (!disableCond) {
                        $("#addImg_Option1").show();
                        $("#addImg_Option2").show();
                        $("#addImg_Option3").show();
                        $("#addImg_Option4").show();

                        $("#imgOption1_Preview").show();
                        $("#imgOption2_Preview").show();
                        $("#imgOption3_Preview").show();
                        $("#imgOption4_Preview").show();
                        $("#imgCorrectOpt_Preview").show();

                        return;
                    }

                    $("#addImg_Option1").hide();
                    $("#addImg_Option2").hide();
                    $("#addImg_Option3").hide();
                    $("#addImg_Option4").hide();

                    //We will empty all the img element src path
                    resetsImagePreview();

                    //Hide the img element for all options preview
                    //We will empty all the img element src path
                    $("#imgOption1_Preview").hide();
                    $("#imgOption2_Preview").hide();
                    $("#imgOption3_Preview").hide();
                    $("#imgOption4_Preview").hide();
                    $("#imgCorrectOpt_Preview").hide();

                }

                // Grab the files and set them to our variable
                function prepareUpload(event) {
                    var files = event.target.files;

                    if (typeof files !== 'undefined' && files.length > 0) {
                        //This represents that the user selects a file to upload
                        //We shall start uploading the file
                        uploadFiles(files, event.target.name);
                    }
                }

                // Catch the form submit and upload the files
                function uploadFiles(fileToUpload, fileInputName) {


                    /*event.stopPropagation(); // Stop stuff happening
                    event.preventDefault(); // Totally stop stuff happening*/

                    // START A LOADING SPINNER HERE

                    // Create a formdata object and add the files
                    var data = new FormData();
                    $.each(fileToUpload, function (key, value) {
                        data.append(key, value);
                    });

                    $.ajax({
                        url: '../FileUpload/Upload',
                        type: 'POST',
                        data: data,
                        cache: false,
                        dataType: 'json',
                        processData: false, // Don't process the files
                        contentType: false, // Set content type to false as jQuery will tell the server its a query string request
                        success: function (data, textStatus, jqXHR) {
                            if (data != "Error ") {
                                // Set the option textfield bound to the model object
                                assignPictOption_Val(fileInputName, data[0].m_FileName);
                            }
                            else {
                                // Handle errors here
                                console.log('ERRORS: ');
                            }
                        },
                        error: function (jqXHR, textStatus, errorThrown) {
                            // Handle errors here
                            console.log('ERRORS: ' + textStatus);
                            // STOP LOADING SPINNER
                        }
                    });
                }

                function assignPictOption_Val(imgOptionName, fileName) {
                    var optionName_Tuple;
                    //var imgOptionID = "";

                    if ((optionName_Tuple = checkOptionName(imgOptionName)) != 0) {
                        optionName_Tuple = optionName_Tuple.split(",");
                        $("#" + optionName_Tuple[0]).val(fileName);
                        $("#"+ optionName_Tuple[1]).attr("src","../Upload/" + fileName);
                        //imgOptionID = 
                    }

                }

                function checkOptionName(imgOptionName) {
                    var optionName_Tuple = 0;
                    if (typeof imgOptionName !== 'undefined' && imgOptionName.length > 0) {
                        if (imgOptionName == "option1_img") {
                            optionName_Tuple = "option_1,imgOption1_Preview";
                        }
                        else if (imgOptionName == "option2_img") {
                            optionName_Tuple = "option_2,imgOption2_Preview";
                        }
                        else if (imgOptionName == "option3_img") {
                            optionName_Tuple = "option_3,imgOption3_Preview";
                        }
                        else if (imgOptionName == "option4_img") {
                            optionName_Tuple = "option_4,imgOption4_Preview";
                        }

                    } // End of outer if code block

                    return optionName_Tuple;
                }


            }); //End of $(document).ready() function body

$("input[name='question_has_hint']").change(function () {
    var hasHint = $("input[name='question_has_hint']:checked").val();
    
    if(typeof(hasHint)!== 'undefined')
    {

        $("input[name='hint']").prop("readonly", (hasHint != "YES"));

        if(hasHint=="NO")
        {
            $("input[name='hint']").val("-NA-");
        }
 
    }
                        
});