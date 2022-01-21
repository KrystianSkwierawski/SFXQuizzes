import { elements, elementStrings } from './base.js';
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
export const getVolumeInputValue = (audioPlayer) => {
    const volumeInput = audioPlayer.querySelector(elementStrings.volumeInput);
    return volumeInput.value;
};
export const showLinkVolumeButton = () => {
    elements.linkVolumeButtons.forEach(button => {
        button.classList.remove('d-none');
    });
    elements.unlinkVolumeButtons.forEach(button => {
        button.classList.add('d-none');
    });
};
export const showUnlinkVolumeButton = () => {
    elements.linkVolumeButtons.forEach(button => {
        button.classList.add('d-none');
    });
    elements.unlinkVolumeButtons.forEach(button => {
        button.classList.remove('d-none');
    });
};
export const setInputToAnsweredCorrectly = (input) => {
    input.classList.add('text-success');
    input.classList.remove('text-danger');
    input.setAttribute('disabled', "");
};
export const addOnePointToCurrentScore = () => {
    const currentScore = +elements.quiz__currentScore.innerHTML;
    elements.quiz__currentScore.innerHTML = (currentScore + 1).toString();
};
export const setInputToAnsweredInCorrectly = (input) => {
    input.classList.add('text-danger');
};
//# sourceMappingURL=quizCreatorView.js.map