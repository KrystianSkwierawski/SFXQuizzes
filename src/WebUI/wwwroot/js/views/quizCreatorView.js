import { elements } from './base.js';
export var selectedFilesStatus = function (fileList) {
    var markup = (fileList.length) > 1 ? "<p>".concat(fileList.length, " files selected</p>") : "<p>".concat(fileList[0].name, " selected</p>");
    elements.dropArea__status.innerHTML = markup;
};
export var addDragEnterBC = function () {
    elements.dropArea.classList.add('drag_enter');
};
export var removeDragEnterBC = function () {
    elements.dropArea.classList.remove('drag_enter');
};
//# sourceMappingURL=quizCreatorView.js.map