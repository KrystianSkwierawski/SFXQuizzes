import { elements } from './views/base.js';
import * as quizCreatorView from './views/quizCreatorView.js';

elements.dropArea.addEventListener('drop', (e: any) => {
    e.preventDefault();

    const fileList: FileList = e.dataTransfer.files;

    (<HTMLInputElement>elements.filesInput).files = fileList;

    quizCreatorView.selectedFilesStatus(fileList);
});

elements.filesInput.addEventListener('change', () => {
    const fileList: FileList = (<HTMLInputElement>elements.filesInput).files;

    quizCreatorView.selectedFilesStatus(fileList);
});

elements.dropArea.addEventListener('dragenter', (e: DragEvent) => {
    e.preventDefault();
    elements.dropArea.classList.add('drag_enter');
});

elements.dropArea.addEventListener('dragleave', (e: DragEvent) => {
    e.preventDefault();
    elements.dropArea.classList.remove('drag_enter');
});


elements.dropArea.addEventListener('dragover', (e: DragEvent) => {
    e.preventDefault();
});

elements.dropArea.addEventListener('dragstart', (e: DragEvent) => {
    e.preventDefault();
    e.dataTransfer.setData("file", "data");
});

elements.browseFilesButton.addEventListener('click', () => {
    (<HTMLInputElement>elements.filesInput).click();
});



