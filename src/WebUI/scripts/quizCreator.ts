import { elements } from './views/base.js';
import * as quizCreatorView from './views/quizCreatorView.js';
import { validateFiles } from './models/Files.js';

elements.dropArea.addEventListener('drop', (e: any) => {
    e.preventDefault();
    quizCreatorView.removeDragEnterBC();

    const fileList: FileList = e.dataTransfer.files;

    const validationError = validateFiles(fileList);
    if (validationError)
        return alert(validationError);


    (<HTMLInputElement>elements.filesInput).files = fileList;

    quizCreatorView.selectedFilesStatus(fileList);
});

elements.filesInput.addEventListener('change', (e) => {
    const fileList: FileList = (<HTMLInputElement>elements.filesInput).files;

    const validationError: string = validateFiles(fileList);
    if (validationError) {
        // reset input
        (<HTMLInputElement>elements.filesInput).files = new DataTransfer().files;

        return alert(validationError);
    }

    quizCreatorView.selectedFilesStatus(fileList);
});

elements.dropArea.addEventListener('dragenter', (e: DragEvent) => {
    e.preventDefault();
    quizCreatorView.addDragEnterBC();
});

elements.dropArea.addEventListener('dragleave', (e: DragEvent) => {
    e.preventDefault();
    quizCreatorView.removeDragEnterBC();
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



