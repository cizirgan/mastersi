var SpeechRecognition = SpeechRecognition || webkitSpeechRecognition

var recognitionMehmet = new SpeechRecognition();


recognitionMehmet.continuous = true;
recognitionMehmet.lang = 'en-US';
recognitionMehmet.interimResults = false;
recognitionMehmet.maxAlternatives = 1;

var diagnostic = document.querySelector('#resultBox');
var statusBox = document.querySelector('#alertX');


/*
document.body.onclick = function() {
    recognition.start();
    console.log('Ready to receive a color command.');
}*/

recognitionMehmet.onresult = function(event) {
    var last = event.results.length - 1;
    var lastTranscript = event.results[last][0].transcript;

    diagnostic.textContent = lastTranscript;
    console.log('Confidence: ' + event.results[0][0].confidence);

    statusBox.style.display = "none"
}

recognitionMehmet.onspeechend = function() {
    recognitionMehmet.stop();
}

recognitionMehmet.onerror = function(event) {
    diagnostic.textContent = 'Error occurred in recognition: ' + event.error;
}

function stopButtonSimple(event) {
    recognitionMehmet.stop();
    statusBox.style.display = "none"
}

function startButtonSimple(event) {
    recognitionMehmet.start();
    statusBox.style.display = "block"
}