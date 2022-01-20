import { elements } from './views/base.js';
import * as quizCreatorView from './views/quizCreatorView.js';
let _audio = new Audio();
let _linkedVolumes;
elements.audioPlayer__startButtons.forEach(button => {
    button.addEventListener('click', (e) => {
        const id = button.id;
        if (!_audio.ended)
            _audio.pause();
        const url = window.location;
        if (url.search)
            _audio.src = `./assets/audios/${url.search}/${id}.wav`;
        _audio.src = `./assets/audios/demo/${id}.wav`;
        const volume = +quizCreatorView.getVolumeInputValue(button.parentElement) / 100;
        _audio.volume = volume;
        _audio.play();
    });
});
elements.linkVolumeButtons.forEach(button => {
    button.addEventListener('click', () => {
        quizCreatorView.showUnlinkVolumeButton();
        _linkedVolumes = false;
    });
});
elements.unlinkVolumeButtons.forEach(button => {
    button.addEventListener('click', () => {
        quizCreatorView.showLinkVolumeButton();
        _linkedVolumes = true;
    });
});
elements.volumeInputs.forEach(input => {
    input.addEventListener('input', () => {
        const volumeInputValue = input.value;
        _audio.volume = +volumeInputValue / 100;
        if (!_linkedVolumes)
            return;
        setLinkedVolumeInputs(volumeInputValue);
    });
});
function setLinkedVolumeInputs(volume) {
    elements.volumeInputs.forEach(input => {
        input.value = volume;
    });
}
//# sourceMappingURL=audioPlayer.js.map