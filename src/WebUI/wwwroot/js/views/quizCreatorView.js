import { elements } from './base.js';
export const selectedFilesStatus = (fileList) => {
    const markup = (fileList.length) > 1 ? `<p>${fileList.length} files selected</p>` : `<p>${fileList[0].name} selected</p>`;
    elements.dropArea__status.innerHTML = markup;
};
export const addDragEnterBC = () => {
    elements.dropArea.classList.add('drag_enter');
};
export const removeDragEnterBC = () => {
    elements.dropArea.classList.remove('drag_enter');
};
export const resetFilesInput = () => {
    elements.filesInput.files = new DataTransfer().files;
};
export const setFilesInputValue = (files) => {
    elements.filesInput.files = files;
};
export const getFilesInputValue = () => {
    return elements.filesInput.files;
};
//# sourceMappingURL=quizCreatorView.js.map