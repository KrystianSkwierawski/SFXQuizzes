import { elements } from './views/base.js';
import * as quizCreatorView from './views/quizCreatorView.js';

let _audio: HTMLAudioElement = new Audio();
let _linkedVolumes: boolean;

elements.audioPlayer__startButtons.forEach(button => {
    button.addEventListener('click', () => playAudio(button));
});

function playAudio(playAudioButton) {
    const sfxId: string = (<HTMLElement>playAudioButton.parentNode).id;

    if (!_audio.ended)
        _audio.pause();

    const url: Location = window.location;

    if (url.search)
        _audio.src = `./assets/audios/${url.search}/${sfxId}.wav`;

    _audio.src = `./assets/audios/demo/${sfxId}.wav`;

    const volume: number = +quizCreatorView.getVolumeInputValue(playAudioButton.parentElement) / 100;
    _audio.volume = volume;

    _audio.play();
}


elements.linkVolumeButtons.forEach(button => {
    button.addEventListener('click', unlinkVolumeButtons);
});

function unlinkVolumeButtons() {
    quizCreatorView.showUnlinkVolumeButton();
    _linkedVolumes = false;
}


elements.unlinkVolumeButtons.forEach(button => {
    button.addEventListener('click', linkVolumeButtons);
});

function linkVolumeButtons() {
    quizCreatorView.showLinkVolumeButton();
    _linkedVolumes = true;
}


elements.volumeInputs.forEach(input => {
    input.addEventListener('input', () => changeVolume(input));
});

function changeVolume(volumeInput) {
    const volumeInputValue: string = (<HTMLInputElement>volumeInput).value;

    _audio.volume = +volumeInputValue / 100;

    if (!_linkedVolumes)
        return;

    setLinkedVolumeInputs(volumeInputValue);
}

function setLinkedVolumeInputs(volume: string) {
    elements.volumeInputs.forEach(input => {
        (<HTMLInputElement>input).value = volume;
    })
}


elements.sfxNameInputs.forEach(input => {
    input.addEventListener('keyup', (e: any) => handleUsersSfxNameGuess(e, input));
});

function handleUsersSfxNameGuess(e, nameInput) {
    if (e.key === 'Enter' || e.keyCode === 13) {
        const sfxId: string = (<HTMLElement>nameInput.parentNode).id;

        const userAnswer: string = (<string>e.target.value).toLowerCase();
        const answer: string = (<any>window).answers.get(sfxId).toLowerCase();

        const isAnswerCorrect: boolean = answer === userAnswer;

        if (isAnswerCorrect) {
            quizCreatorView.setInputToAnsweredCorrectly(nameInput);
            quizCreatorView.addOnePointToCurrentScore();

            return;
        }

        quizCreatorView.setInputToAnsweredInCorrectly(nameInput);
    }
}


elements.quiz__endQuizButton.addEventListener('click', quizCreatorView.setAllAnswers);




