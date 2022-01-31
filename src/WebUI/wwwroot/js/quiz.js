import { elements } from './views/base.js';
import * as quizView from './views/quizView.js';
import { isAnswerCorrect } from './models/Quiz.js';
var _audio = new Audio();
var _linkedVolumes;
elements.sfxPlayer__startButtons.forEach(function (button) {
    button.addEventListener('click', function () { return playAudio(button); });
});
function playAudio(playAudioButton) {
    var sfxId = playAudioButton.parentNode.id;
    var quizId = quizView.getQuizId();
    var sfxName = window.answers.get(sfxId);
    if (!_audio.ended)
        _audio.pause();
    _audio.src = quizId ? "/assets/SFXs/".concat(quizId, "/").concat(sfxName) : "/assets/SFXs/demo/".concat(sfxName);
    var volume = +quizView.getVolumeInputValue(playAudioButton.parentElement) / 100;
    _audio.volume = volume;
    _audio.play();
}
elements.linkVolumeButtons.forEach(function (button) {
    button.addEventListener('click', unlinkVolumeButtons);
});
function unlinkVolumeButtons() {
    quizView.showUnlinkVolumeButton();
    _linkedVolumes = false;
}
elements.unlinkVolumeButtons.forEach(function (button) {
    button.addEventListener('click', linkVolumeButtons);
});
function linkVolumeButtons() {
    quizView.showLinkVolumeButton();
    _linkedVolumes = true;
}
elements.volumeInputs.forEach(function (input) {
    input.addEventListener('input', function () {
        //TODO: if playing current sfx     
        changeVolume(input);
    });
});
function changeVolume(volumeInput) {
    var volumeInputValue = volumeInput.value;
    _audio.volume = +volumeInputValue / 100;
    if (!_linkedVolumes)
        return;
    setLinkedVolumeInputs(volumeInputValue);
}
function setLinkedVolumeInputs(volume) {
    elements.volumeInputs.forEach(function (input) {
        input.value = volume;
    });
}
elements.sfxNameInputs.forEach(function (input) {
    input.addEventListener('keyup', function (e) { return handleUsersSfxNameGuess(e, input); });
});
function handleUsersSfxNameGuess(e, nameInput) {
    if (!(e.key === 'Enter') || !(e.keyCode === 13))
        return;
    var sfxId = nameInput.parentNode.id;
    var userAnswer = e.target.value.toLowerCase();
    if (isAnswerCorrect(sfxId, userAnswer)) {
        quizView.setInputToAnsweredCorrectly(nameInput);
        quizView.addOnePointToCurrentScore();
        return;
    }
    quizView.setInputToAnsweredInCorrectly(nameInput);
}
elements.quiz__endQuizButton.addEventListener('click', quizView.setAllAnswers);
//# sourceMappingURL=quiz.js.map