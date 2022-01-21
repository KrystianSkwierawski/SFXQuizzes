import { elements } from './views/base.js';
import * as sfxPlayerView from './views/sfxPlayerView.js';
let _audio = new Audio();
let _linkedVolumes;
elements.sfxPlayer__startButtons.forEach(button => {
    button.addEventListener('click', () => playAudio(button));
});
function playAudio(playAudioButton) {
    const sfxId = playAudioButton.parentNode.id;
    const quizId = window.quizId;
    const sfxName = window.answers.get(sfxId);
    if (!_audio.ended)
        _audio.pause();
    if (quizId)
        _audio.src = `/assets/SFXs/${quizId}/${sfxName}`;
    if (!quizId)
        _audio.src = `/assets/SFXs/demo/${sfxName}`;
    const volume = +sfxPlayerView.getVolumeInputValue(playAudioButton.parentElement) / 100;
    _audio.volume = volume;
    console.log(_audio.src);
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
            sfxPlayerView.setInputToAnsweredCorrectly(nameInput);
            sfxPlayerView.addOnePointToCurrentScore();
            return;
        }
        sfxPlayerView.setInputToAnsweredInCorrectly(nameInput);
    }
}
elements.quiz__endQuizButton.addEventListener('click', sfxPlayerView.setAllAnswers);
//# sourceMappingURL=sfxPlayer.js.map