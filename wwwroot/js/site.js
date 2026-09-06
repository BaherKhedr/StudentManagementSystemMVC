function LinkwithOneRowOutPut(olddivname , classname) {

    const link = document.querySelector("." + classname);

    const olddiv = document.querySelector("#" + olddivname);

        link.addEventListener("click", function (event) {
            event.preventDefault();

            const URL = link.href;
            fetch(URL)
                .then(function (response) {
                    return response.text();
                })
                .then(function (html) {

                    olddiv.innerHTML = html;
                });
        });
}
function setupPaginationLinks(replacedstudentlistid, replacedpaginationid) {

    const studentslist = document.querySelector("#" + replacedstudentlistid);
    const pagination = document.querySelector("#" + replacedpaginationid);

    const paginationLinks = pagination.querySelectorAll(".pagination-link");

    for (let i = 0; i < paginationLinks.length; i++) {
        const link = paginationLinks[i];

        link.addEventListener("click", function (event) {
            event.preventDefault();

            const url = link.href;

            fetch(url)
                .then(function (response) {
                    return response.text();
                })
                .then(function (html) {

                    const parser = new DOMParser();
                    const doc = parser.parseFromString(html, "text/html");

                    const newstudentslist = doc.querySelector(".students-table");
                    const newpagination = doc.querySelector(".pagination-container");

                    studentslist.innerHTML = newstudentslist.outerHTML;
                    pagination.innerHTML = newpagination.outerHTML;

                    setupPaginationLinks(replacedstudentlistid, replacedpaginationid);
                });
        });
    }
}
function PaginationForForm(FormClass, replacedDiv1, replacedDiv2) {
    const form = document.querySelector("." + FormClass);
    form.addEventListener("submit", function (event) {
        event.preventDefault();

        const formdata = new FormData(form);

        const params = new URLSearchParams(formdata);

        const url = form.action;

        const queryString = params.toString();

        const fullURL = url + "?" + queryString;
        fetch(fullURL)
            .then(function (response) {
                return response.text();
            })
            .then(function (html) {

                const div1 = document.querySelector("#" + replacedDiv1);
                const div2 = document.querySelector("#" + replacedDiv2);

                const parser = new DOMParser();
                const doc = parser.parseFromString(html, "text/html");

                const newstudentslist = doc.querySelector(".students-table");
                const newpagination = doc.querySelector(".pagination-container");

                div1.innerHTML = newstudentslist.outerHTML;
                div2.innerHTML = newpagination.outerHTML;

                setupPaginationLinks(replacedDiv1, replacedDiv2);
            });
    });
}

function LinkwithMoreThanOneRowOutPut(ReplacedDiv1Id, ReplacedDiv2Id , ClassName) {
    const link = document.querySelector("." + ClassName);

    const oldDiv1 = document.querySelector("#" + ReplacedDiv1Id);
    const oldDiv2 = document.querySelector("#" + ReplacedDiv2Id);

    link.addEventListener("click", function (event) {
        event.preventDefault();

        const URL = link.href;
        fetch(URL).then(function (response) {
            return response.text();
        }).then(function (html) {

            const Dom = new DOMParser();
            const doc = Dom.parseFromString(html, "text/html");

            const newstudentlist = doc.querySelector(".students-table");
            const newpagination = doc.querySelector(".pagination-container");

            oldDiv1.innerHTML = newstudentlist.outerHTML;
            oldDiv2.innerHTML = newpagination.outerHTML;

            setupPaginationLinks(ReplacedDiv1Id, ReplacedDiv2Id);

        });
    });
}
