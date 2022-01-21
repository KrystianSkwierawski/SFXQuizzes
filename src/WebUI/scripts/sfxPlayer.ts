import { elements } from './views/base.js';
import * as sfxPlayerView from './views/sfxPlayerView.js';

let _audio: HTMLAudioElement = new Audio();
let _linkedVolumes: boolean;

elements.sfxPlayer__startButtons.forEach(button => {
    button.addEventListener('click', () => playAudio(button));
});

function playAudio(playAudioButton) {
    const sfxId: string = (<HTMLElement>playAudioButton.parentNode).id;

    if (!_audio.ended)
        _audio.pause();

    const url: Location = window.location;

    if (url.search)
        _audio.src = `./assets/SFXs/${url.search}/${sfxId}.wav`;

    _audio.src = `./assets/SFXs/demo/${sfxId}.wav`;

    const volume: number = +sfxPlayerView.getVolumeInputValue(playAudioButton.parentElement) / 100;
    _audio.volume = volume;

    _audio.play();
}


elements.linkVolumeButtons.forEach(button => {
    button.addEventListener('click', unlinkVolumeButtons);
});

function unlinkVolumeButtons() {
    sfxPlayerView.showUnlinkVolumeButton();
    _linkedVolumes = false;
}


elements.unlinkVolumeButtons.forEach(button => {
    button.addEventListener('click', linkVolumeButtons);
});

function linkVolumeButtons() {
    sfxPlayerView.showLinkVolumeButton();
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
            sfxPlayerView.setInputToAnsweredCorrectly(nameInput);
            sfxPlayerView.addOnePointToCurrentScore();

            return;
        }

        sfxPlayerView.setInputToAnsweredInCorrectly(nameInput);
    }
}


elements.quiz__endQuizButton.addEventListener('click', sfxPlayerView.setAllAnswers);




