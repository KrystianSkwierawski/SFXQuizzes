import { elements } from './views/base.js';
import * as quizCreatorView from './views/quizCreatorView.js';
import { validateFiles } from './models/Files.js';
elements.dropArea.addEventListener('drop', (e) => {
    e.preventDefault();
    quizCreatorView.removeDragEnterBC();
    const fileList = e.dataTransfer.files;
    const validationError = validateFiles(fileList);
    if (validationError)
        return alert(validationError);
    quizCreatorView.setFilesInputValue(fileList);
    quizCreatorView.selectedFilesStatus(fileList);
});
elements.filesInput.addEventListener('change', (e) => {
    const fileList = quizCreatorView.getFilesInputValue();
    const validationError = validateFiles(fileList);
    if (validationError) {
        quizCreatorView.resetFilesInput();
        return alert(validationError);
    }
    quizCreatorView.selectedFilesStatus(fileList);
});
elements.dropArea.addEventListener('dragenter', (e) => {
    e.preventDefault();
    quizCreatorView.addDragEnterBC();
});
elements.dropArea.addEventListener('dragleave', (e) => {
    e.preventDefault();
    quizCreatorView.removeDragEnterBC();
});
elements.dropArea.addEventListener('dragover', (e) => {
    e.preventDefault();
});
elements.dropArea.addEventListener('dragstart', (e) => {
    e.preventDefault();
    e.dataTransfer.setData("file", "data");
});
elements.browseFilesButton.addEventListener('click', () => {
    elements.filesInput.click();
});
