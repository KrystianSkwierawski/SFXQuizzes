import { elements, elementStrings } from './base.js';
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
export const setAllAnswers = function () {
    elements.sfxNameInputs.forEach((input) => {
        const sfxId = input.parentNode.id;
        const answer = window.answers.get(sfxId).split('.')[0].toLowerCase();
        input.setAttribute("disabled", "");
        input.value = answer;
    });
};
//# sourceMappingURL=sfxPlayerView.js.map