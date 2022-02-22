import { elements, elementStrings } from './base.js';
export const getVolumeInputValue = (audioPlayer) => {
    const volumeInput = audioPlayer.querySelector(elementStrings.volumeInput);
    return volumeInput.value;
};
export const getQuizId = () => {
    return elements.quizId.value;
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
export const showPlayAudioButtons = () => {
    elements.sfxPlayer__playButtons.forEach(button => {
        if (button.classList.contains('d-none'))
            button.classList.remove('d-none');
    });
    elements.sfxPlayer__pauseButtons.forEach(button => {
        if (!button.classList.contains('d-none'))
            button.classList.add('d-none');
    });
};
export const showPlayAudioButton = (sfxPlayerEl) => {
    const playAudioEl = sfxPlayerEl.querySelector(elementStrings.sfxPlayer__playButton);
    playAudioEl.classList.remove('d-none');
    const pauseAudioEl = sfxPlayerEl.querySelector(elementStrings.sfxPlayer__pauseButton);
    pauseAudioEl.classList.add('d-none');
};
export const showPauseAudioButton = (sfxPlayerEl) => {
    const playAudioEl = sfxPlayerEl.querySelector(elementStrings.sfxPlayer__playButton);
    playAudioEl.classList.add('d-none');
    const pauseAudioEl = sfxPlayerEl.querySelector(elementStrings.sfxPlayer__pauseButton);
    pauseAudioEl.classList.remove('d-none');
};
export const setInputToAnsweredCorrectly = (input) => {
    input.classList.add('text-success');
    input.classList.remove('text-danger');
    input.setAttribute('disabled', "");
    const sfxId = input.parentElement.id;
    const answer = window.answers.get(sfxId).split('.')[0].split(' ` ')[0];
    input.value = answer;
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
        const sfxId = input.parentElement.id;
        const answer = window.answers.get(sfxId).split('.')[0].split(' ` ')[0];
        input.setAttribute("disabled", "");
        input.value = answer;
    });
};
//# sourceMappingURL=quizView.js.map