import { elements } from './base.js';
//export const uploadingFilesStatus = () => {
//    elements.browseFilesButton.innerHTML = `<p>Uploading files...</p>`;
//};
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
export const getVolumeInputValue = () => {
    return elements.volumeInput.value;
};
//# sourceMappingURL=quizCreatorView.js.map