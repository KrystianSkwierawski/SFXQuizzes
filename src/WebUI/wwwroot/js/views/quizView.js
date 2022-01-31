import { elements, elementStrings } from './base.js';
export var getVolumeInputValue = function (audioPlayer) {
    var volumeInput = audioPlayer.querySelector(elementStrings.volumeInput);
    return volumeInput.value;
};
export var getQuizId = function () {
    return elements.quizId.value;
};
export var showLinkVolumeButton = function () {
    elements.linkVolumeButtons.forEach(function (button) {
        button.classList.remove('d-none');
    });
    elements.unlinkVolumeButtons.forEach(function (button) {
        button.classList.add('d-none');
    });
};
export var showUnlinkVolumeButton = function () {
    elements.linkVolumeButtons.forEach(function (button) {
        button.classList.add('d-none');
    });
    elements.unlinkVolumeButtons.forEach(function (button) {
        button.classList.remove('d-none');
    });
};
export var setInputToAnsweredCorrectly = function (input) {
    input.classList.add('text-success');
    input.classList.remove('text-danger');
    input.setAttribute('disabled', "");
};
export var addOnePointToCurrentScore = function () {
    var currentScore = +elements.quiz__currentScore.innerHTML;
    elements.quiz__currentScore.innerHTML = (currentScore + 1).toString();
};
export var setInputToAnsweredInCorrectly = function (input) {
    input.classList.add('text-danger');
};
export var setAllAnswers = function () {
    elements.sfxNameInputs.forEach(function (input) {
        var sfxId = input.parentNode.id;
        var answer = window.answers.get(sfxId).split('.')[0].split(' ` ')[0];
        input.setAttribute("disabled", "");
        input.value = answer;
    });
};
//# sourceMappingURL=quizView.js.map