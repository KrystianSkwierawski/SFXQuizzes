import { elements, elementStrings } from './base.js';
import { getBreakingMark } from '../models/Files.js';


export const getVolumeInputValue = (audioPlayer: HTMLElement) => {
    const volumeInput: HTMLInputElement = audioPlayer.querySelector(elementStrings.volumeInput);
    return volumeInput.value;
};

export const getQuizId = () => {
    return (<HTMLInputElement>elements.quizId).value;
}

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


export const showPlayAudioButton = (sfxPlayerEl: HTMLElement) => {
    const playAudioEl: HTMLElement = sfxPlayerEl.querySelector(elementStrings.sfxPlayer__playButton);
    playAudioEl.classList.remove('d-none');

    const pauseAudioEl: HTMLElement = sfxPlayerEl.querySelector(elementStrings.sfxPlayer__pauseButton);
    pauseAudioEl.classList.add('d-none');
};

export const showPauseAudioButton = (sfxPlayerEl: HTMLElement) => {
    const playAudioEl: HTMLElement = sfxPlayerEl.querySelector(elementStrings.sfxPlayer__playButton);
    playAudioEl.classList.add('d-none');

    const pauseAudioEl: HTMLElement = sfxPlayerEl.querySelector(elementStrings.sfxPlayer__pauseButton);
    pauseAudioEl.classList.remove('d-none');
};

export const setInputToAnsweredCorrectly = (input: HTMLInputElement) => {
    input.classList.add('text-success');
    input.classList.remove('text-danger');
    input.setAttribute('disabled', "");

    const sfxId: string = input.parentElement.id;

    let answer: string = (<any>window).answers.get(sfxId);
    const breakingMark = getBreakingMark(answer);
    answer = answer.split(breakingMark)[0];
    input.value = answer;
};

export const addOnePointToCurrentScore = () => {
    const currentScore: number = +elements.quiz__currentScore.innerHTML;
    elements.quiz__currentScore.innerHTML = (currentScore + 1).toString();
};

export const setInputToAnsweredInCorrectly = (input: HTMLElement) => {
    input.classList.add('text-danger');
};

export const setAllAnswers = function () {
    elements.sfxNameInputs.forEach((input: HTMLInputElement) => {
        const sfxId: string = input.parentElement.id;

        let answer: string = (<any>window).answers.get(sfxId);
        const breakingMark = getBreakingMark(answer);
        answer = answer.split(breakingMark)[0];

        input.setAttribute("disabled", "");
        input.value = answer;
    });
}
