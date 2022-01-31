import { elements } from './views/base.js';
import * as quizCreatorView from './views/quizCreatorView.js';
import { validateFiles } from './models/Files.js';
elements.dropArea.addEventListener('drop', function (e) {
    e.preventDefault();
    quizCreatorView.removeDragEnterBC();
    var fileList = e.dataTransfer.files;
    var validationError = validateFiles(fileList);
    if (validationError)
        return alert(validationError);
    quizCreatorView.setFilesInputValue(fileList);
    quizCreatorView.selectedFilesStatus(fileList);
});
elements.filesInput.addEventListener('change', function (e) {
    var fileList = quizCreatorView.getFilesInputValue();
    var validationError = validateFiles(fileList);
    if (validationError) {
        quizCreatorView.resetFilesInput();
        return alert(validationError);
    }
    quizCreatorView.selectedFilesStatus(fileList);
});
elements.dropArea.addEventListener('dragenter', function (e) {
    e.preventDefault();
    quizCreatorView.addDragEnterBC();
});
elements.dropArea.addEventListener('dragleave', function (e) {
    e.preventDefault();
    quizCreatorView.removeDragEnterBC();
});
elements.dropArea.addEventListener('dragover', function (e) {
    e.preventDefault();
});
elements.dropArea.addEventListener('dragstart', function (e) {
    e.preventDefault();
    e.dataTransfer.setData("file", "data");
});
elements.browseFilesButton.addEventListener('click', function () {
    elements.filesInput.click();
});
//# sourceMappingURL=quizCreator.js.map