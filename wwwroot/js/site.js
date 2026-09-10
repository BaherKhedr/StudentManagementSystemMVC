
function paginationOperation(replacedstudentlistid, replacedpaginationid) {


    return function (html) {
        const studentslist = document.querySelector("#" + replacedstudentlistid);
        const pagination = document.querySelector("#" + replacedpaginationid);

        const parser = new DOMParser();
        const doc = parser.parseFromString(html, "text/html");

        const newstudentslist = doc.querySelector(".students-table");
        const newpagination = doc.querySelector(".pagination-container");

        studentslist.innerHTML = newstudentslist.outerHTML;
        pagination.innerHTML = newpagination.outerHTML;

    }
}
function SearchOperation(replacedDivStudent, replacedDivPagination, replacedDivValidation) {

    return function (html) {

        const divStudent = document.querySelector("#" + replacedDivStudent);
        const divPagination = document.querySelector("#" + replacedDivPagination);
        const divValidation = document.querySelector("#" + replacedDivValidation);

        const parser = new DOMParser();
        const doc = parser.parseFromString(html, "text/html");

        const newstudentslist = doc.querySelector(".students-table");
        const newpagination = doc.querySelector(".pagination-container");
        const newvalidation = doc.querySelector(".newvalidation")

        if (newvalidation == null) {
            divStudent.innerHTML = newstudentslist.outerHTML;
            divPagination.innerHTML = newpagination.outerHTML;
            divValidation.innerHTML = "";
        }

        else if (newstudentslist == null && newpagination == null) {
            divValidation.innerHTML = newvalidation.outerHTML;
            divStudent.innerHTML = "";
            divPagination.innerHTML = "";
        }

    }
}
function OneRowOperation(replacedDiv) {

    return function (html) { // closure

        const olddiv = document.querySelector("#" + replacedDiv);

        olddiv.innerHTML = html;
    }
}
function MultipleRowsOperation(ReplacedDiv1Id, ReplacedDiv2Id) {

    return function (html) {

        const oldDiv1 = document.querySelector("#" + ReplacedDiv1Id);
        const oldDiv2 = document.querySelector("#" + ReplacedDiv2Id);

        const Dom = new DOMParser();
        const doc = Dom.parseFromString(html, "text/html");

        const newstudentlist = doc.querySelector(".students-table");
        const newpagination = doc.querySelector(".pagination-container");

        oldDiv1.innerHTML = newstudentlist.outerHTML;
        oldDiv2.innerHTML = newpagination.outerHTML;

    }
}
function AddOperation(replacedDivStudent, replacedDivValidation) {
    return function (html) {

        const oldStudentDiv = document.querySelector("#" + replacedDivStudent);
        const oldValidationDiv = document.querySelector("#" + replacedDivValidation);

        const parser = new DOMParser();
        const doc = parser.parseFromString(html, "text/html");

        const validation = doc.querySelector(".validation");
        const student = doc.querySelector(".table");

        if (student == null) {
            oldValidationDiv.innerHTML = validation.outerHTML;
            oldStudentDiv.innerHTML = "";
        }
        else {
            oldStudentDiv.innerHTML = student.outerHTML;
            oldValidationDiv.innerHTML = "";
        }
    }
}

function AjaxRequest(Class, eventType, Operation, methodtype , rebind , rebindClass ,rebindEventType, rebindOperation , rebindMethod) {

    const elements = document.querySelectorAll("." + Class);

    for (let i = 0; i < elements.length; i++) {
        let element = elements[i];

        element.addEventListener(eventType, function (event) {
            event.preventDefault();

            let fullURL;

            let formdata;
            let requestoptions = {};

            if (eventType === "submit") {

                const url = element.action;

                formdata = new FormData(element);

                if (methodtype === "GET") {

                    const params = new URLSearchParams(formdata);

                    const queryString = params.toString();

                    fullURL = url + "?" + queryString;

                    requestoptions.method = methodtype;
                }
                else if (methodtype === "POST") {

                    fullURL = url;

                    requestoptions.method = methodtype;
                    requestoptions.body = formdata;
                }
            }

            else if (eventType === "click") {

                fullURL = element.href;
                requestoptions.method = methodtype;
            }

            fetch(fullURL, requestoptions)
                .then(function (response) {
                    return response.text();
                })
                .then(function (html) {
                    Operation(html);
                    if (rebind) {

                        const classToBind = rebindClass ?? Class;

                        const operationToBind = rebindOperation ?? Operation;

                        const eventToBind = rebindEventType ?? eventType;

                        const methodToBind = rebindMethod ?? methodtype;

                        AjaxRequest(classToBind, eventToBind, operationToBind, methodToBind, true);
                    }
                    
                });
        });
    }
}
