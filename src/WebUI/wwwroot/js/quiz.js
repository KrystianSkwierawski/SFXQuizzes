import { elements } from './views/base.js';
import * as quizView from './views/quizView.js';
let _audio = new Audio();
let _linkedVolumes;
elements.sfxPlayer__startButtons.forEach(button => {
    button.addEventListener('click', () => playAudio(button));
});
function playAudio(playAudioButton) {
    const sfxId = playAudioButton.parentNode.id;
    const quizId = quizView.getQuizId();
    const sfxName = window.answers.get(sfxId);
    if (!_audio.ended)
        _audio.pause();
    _audio.src = quizId ? `/assets/SFXs/${quizId}/${sfxName}` : `/assets/SFXs/demo/${sfxName}`;
    const volume = +quizView.getVolumeInputValue(playAudioButton.parentElement) / 100;
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
        changeVolume(input);
    });
});
function changeVolume(volumeInput) {
    const volumeInputValue = volumeInput.value;
    _audio.volume = +volumeInputValue / 100;
    if (!_linkedVolumes)
        return;
    setLinkedVolumeInputs(volumeInputValue);
}
function setLinkedVolumeInputs(volume) {
    elements.volumeInputs.forEach(input => {
        input.value = volume;
    });
}
elements.sfxNameInputs.forEach(input => {
    input.addEventListener('keyup', (e) => handleUsersSfxNameGuess(e, input));
});
function handleUsersSfxNameGuess(e, nameInput) {
    if (e.key === 'Enter' || e.keyCode === 13) {
        const sfxId = nameInput.parentNode.id;
        const userAnswer = e.target.value.toLowerCase();
        const answer = window.answers.get(sfxId).split('.')[0].toLowerCase();
        const isAnswerCorrect = answer === userAnswer;
        if (isAnswerCorrect) {
            quizView.setInputToAnsweredCorrectly(nameInput);
            quizView.addOnePointToCurrentScore();
            return;
        }
        quizView.setInputToAnsweredInCorrectly(nameInput);
    }
}
elements.quiz__endQuizButton.addEventListener('click', quizView.setAllAnswers);
//# sourceMappingURL=quiz.js.map