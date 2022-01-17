import { elements } from './views/base.js';
import * as quizCreatorView from './views/quizCreatorView.js';
elements.dropArea.addEventListener('drop', (e) => {
    e.preventDefault();
    const fileList = e.dataTransfer.files;
    elements.filesInput.files = fileList;
    quizCreatorView.selectedFilesStatus(fileList);
    quizCreatorView.removeDragEnterBC();
});
elements.filesInput.addEventListener('change', () => {
    const fileList = elements.filesInput.files;
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
//# sourceMappingURL=quizCreator.js.map