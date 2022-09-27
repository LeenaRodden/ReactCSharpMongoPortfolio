
    function addBulletPoint() {
        let hdnBulletCount = document.getElementById('hdnBulletCount');
    let count = hdnBulletCount.value;
    const input = document.createElement("input");
    input.type = "text";
    input.className = "row form-control";
    input.name = "BulletPoints[" + count + "]";
    input.id = "BulletPoints_" + count + "_";//BulletPoints_0_
    document.getElementById("divBulletPoints").appendChild(input);

    count++;
    hdnBulletCount.value = count;
    }

    function removeBulletPoint() {
        let hdnBulletCount = document.getElementById('hdnBulletCount');
    let count = hdnBulletCount.value;
    count--;
    const id = "BulletPoints_" + count + "_";
    const element = document.getElementById(id);
    if (element === null) {
        document.getElementById('spnErrors').innerHTML = "Cannot remove bullet point";
    return;
        }
    element.remove();
    hdnBulletCount.value = count;
    }

function addTech() {
    let hdnTechCount = document.getElementById('hdnTechCount');
    let count = hdnTechCount.value;


    let html = `
                    <div class="editor-label"><label for="Technologies_` + count + `__TechnologyId">TechnologyId</label></div>
<div class="editor-field"><input class="form-control text-box single-line valid" data-val="true" data-val-number="The field TechnologyId must be a number." data-val-required="The TechnologyId field is required." id="Technologies_` + count + `__TechnologyId" name="Technologies[` + count + `].TechnologyId" type="number" value="` + count + `" aria-describedby="Technologies_` + count + `__TechnologyId-error" aria-invalid="false"> <span class="field-validation-valid" data-valmsg-for="Technologies[` + count + `].TechnologyId" data-valmsg-replace="true"></span></div>
<div class="editor-label"><label for="Technologies_` + count + `__TechnologyName">TechnologyName</label></div>
<div class="editor-field"><input class="form-control text-box single-line" id="Technologies_` + count + `__TechnologyName" name="Technologies[` + count + `].TechnologyName" type="text" value=""> <span class="field-validation-valid" data-valmsg-for="Technologies[` + count + `].TechnologyName" data-valmsg-replace="true"></span></div>
<div class="editor-label"><label for="Technologies_` + count + `__TechnologyType">TechnologyType</label></div>
<div class="editor-field"><input class="form-control text-box single-line" id="Technologies_` + count + `__TechnologyType" name="Technologies[` + count + `].TechnologyType" type="text" value=""> <span class="field-validation-valid" data-valmsg-for="Technologies[` + count + `].TechnologyType" data-valmsg-replace="true"></span></div>
    `;

    const div = document.createElement("div");
    div.className = "row";
    div.id = "divTech" + count;
    div.innerHTML = html;
    document.getElementById("divTechnologies").appendChild(div);

    count++;
    hdnTechCount.value = count;
}

function removeTech() {
    let hdnTechCount = document.getElementById('hdnTechCount');
    let count = hdnTechCount.value;
    count--;
    const id = "divTech" + count;
    const element = document.getElementById(id);
    if (element === null) {
        document.getElementById('spnErrors').innerHTML = "Cannot remove technology";
        return;
    }
    element.remove();
    hdnBulletCount.value = count;
}

function addImage() {
    let hdnImageCount = document.getElementById('hdnImageCount');
    let count = hdnImageCount.value;


    let html = `
<div class="editor-label"><label for="ImageModels_` + count + `__Url">Url</label></div>
<div class="editor-field"><input class="form-control text-box single-line" id="ImageModels_` + count + `__Url" name="ImageModels[` + count + `].Url" type="text" value=""> <span class="field-validation-valid" data-valmsg-for="ImageModels[` + count + `].Url" data-valmsg-replace="true"></span></div>
<div class="editor-label"><label for="ImageModels_` + count + `__AltText">AltText</label></div>
<div class="editor-field"><input class="form-control text-box single-line" id="ImageModels_` + count + `__AltText" name="ImageModels[` + count + `].AltText" type="text" value=""> <span class="field-validation-valid" data-valmsg-for="ImageModels[` + count + `].AltText" data-valmsg-replace="true"></span></div>
    `;

    const div = document.createElement("div");
    div.className = "row";
    div.id = "divImage" + count;
    div.innerHTML = html;
    document.getElementById("divImages").appendChild(div);

    count++;
    hdnTechCount.value = count;
}

function removeImage() {
    let hdnImageCount = document.getElementById('hdnImageCount');
    let count = hdnImageCount.value;
    count--;
    const id = "divImage" + count;
    const element = document.getElementById(id);
    if (element === null) {
        document.getElementById('spnErrors').innerHTML = "Cannot remove image";
        return;
    }
    element.remove();
    hdnImageCount.value = count;
}