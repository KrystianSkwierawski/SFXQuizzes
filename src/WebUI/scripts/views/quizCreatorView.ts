import { elements } from './base.js';

export const selectedFilesStatus = (fileList: FileList): void => {
    const markup = (fileList.length) > 1 ? `<p>${fileList.length} files selected</p>` : `<p>${fileList[0].name} selected</p>`
    elements.dropArea__status.innerHTML = markup;
};

export const addDragEnterBC = (): void => {
    elements.dropArea.classList.add('drag_enter');
}

export const removeDragEnterBC = (): void => {
    elements.dropArea.classList.remove('drag_enter');
}


