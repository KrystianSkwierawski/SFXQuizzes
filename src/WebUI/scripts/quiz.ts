import { elements } from './views/base.js';
import * as quizView from './views/quizView.js';
import { isAnswerCorrect } from './models/Quiz.js';


let _audio: HTMLAudioElement = new Audio();
let _linkedVolumes: boolean;

elements.sfxPlayer__startButtons.forEach(button => {
    button.addEventListener('click', () => playAudio(button));
});

function playAudio(playAudioButton) {
    const sfxId: string = (<HTMLElement>playAudioButton.parentNode).id;
    const quizId: string | undefined = quizView.getQuizId();
    const sfxName: string | undefined = (<any>window).answers.get(sfxId);

    if (!_audio.ended)
        _audio.pause();

    _audio.src = quizId ? `/assets/SFXs/${quizId}/${sfxName}` : `/assets/SFXs/demo/${sfxName}`;

    const volume: number = +quizView.getVolumeInputValue(playAudioButton.parentElement) / 100;
    _audio.volume = volume;

    _audio.play();
}


elements.linkVolumeButtons.forEach(button => {
    button.addEventListener('click', unlinkVolumeButtons);
});

function unlinkVolumeButtons() {
    quizView.showUnlinkVolumeButton();
    _linkedVolumes = false;
}


elements.unlinkVolumeButtons.forEach(button => {
    button.addEventListener('click', linkVolumeButtons);
});

function linkVolumeButtons() {
    quizView.showLinkVolumeButton();
    _linkedVolumes = true;
}


elements.volumeInputs.forEach(input => {
    input.addEventListener('input', () => {
        //TODO: if playing current sfx     
        changeVolume(input)
    });
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
    if (!(e.key === 'Enter') || !(e.keyCode === 13))
        return;

    const sfxId: string = (<HTMLElement>nameInput.parentNode).id;
    const userAnswer: string = (<string>e.target.value).toLowerCase();

    if (isAnswerCorrect(sfxId, userAnswer)) {
        quizView.setInputToAnsweredCorrectly(nameInput);
        quizView.addOnePointToCurrentScore();

        return;
    }

    quizView.setInputToAnsweredInCorrectly(nameInput);
}


elements.quiz__endQuizButton.addEventListener('click', quizView.setAllAnswers);




