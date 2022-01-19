import { elements } from './views/base.js';
let _audio;
elements.audioPlayer__startButtons.forEach(button => {
    button.addEventListener('click', (e) => {
        const id = button.id;
        if (_audio)
            _audio.pause();
        const url = window.location;
        if (url.search)
            _audio = new Audio(`./assets/audios/${url.search}/${id}.wav`);
        _audio = new Audio(`./assets/audios/demo/${id}.wav`);
        _audio.play();
    });
});
//# sourceMappingURL=audioPlayer.js.map