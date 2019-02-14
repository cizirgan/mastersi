$(document).ready(function() {

    var loading = $("#loading");
    $(document).ajaxStart(function () {
        loading.show();
    });
    //noinspection JSUnresolvedFunction
    $(document).ajaxStop(function () {
        loading.hide();
    })
});



function emotionAnalysis(img) {   
    $.ajax({
        url: '/Emotion/GetImageEmotions',
        type: 'POST',
        data: {
            'imageDataBase64': img
        },
        dataType: 'json',
        success: function (data) {
            setEmotionData(data);
           console.log(data.veriler.result);
            $('#emotionJSON').text(JSON.stringify(JSON.parse(data.veriler.result), null, 2));
        }
    });
}

function faceDetection(img) {
    $.ajax({
        url: '/Face/DetectFace',
        type: 'POST',
        data: {
            'imageDataBase64': img
        },
        dataType: 'json',
        success: function (data) {
            setFaceData(data);
          //  console.log(data.veriler.result);
            $('#faceJSON').text(JSON.stringify(JSON.parse(data.veriler.result), null, 2));
        }
    });
}


function fileUpload(img) {

    var myform = document.getElementById("mehmetForm");
    var fd = new FormData(myform);
    fd.append('img', img);

      $.ajax({
        url: '/Emotion/UploadImage',
        type: 'POST',
        data: fd,
        dataType: 'json',
        contentType: false,
        processData: false,
        success: function (data) {
            console.log(JSON.stringify(data));
            $('#mehmet').attr('src', data.name);
     
        }
    });
}


var imageCapture;

function onGetUserMediaButtonClick() {
    navigator.mediaDevices.getUserMedia({ video: true })
        .then(mediaStream => {
            document.querySelector('video').srcObject = mediaStream;

            const track = mediaStream.getVideoTracks()[0];
            imageCapture = new ImageCapture(track);
        })
        .catch(error => console.log(error));
}

function onTakePhotoButtonClick() {
    imageCapture.takePhoto()
        .then(blob => createImageBitmap(blob))
        .then(imageBitmap => {
            const canvas = document.querySelector('#takePhotoCanvas');
            drawCanvas(canvas, imageBitmap);

  
            //Mehmet


            var dataURL = canvas.toDataURL();
            var img = canvas.toDataURL();
            // document.getElementById('PhotoURL').src = img;
            emotionAnalysis(img);
            faceDetection(img);
          
            
        })
        .catch(error => console.log(error));
}


function drawCanvas(canvas, img) {
    canvas.width = getComputedStyle(canvas).width.split('px')[0];
    canvas.height = getComputedStyle(canvas).height.split('px')[0];
    let ratio = Math.min(canvas.width / img.width, canvas.height / img.height);
    let x = (canvas.width - img.width * ratio) / 2;
    let y = (canvas.height - img.height * ratio) / 2;
    canvas.getContext('2d').clearRect(0, 0, canvas.width, canvas.height);
    canvas.getContext('2d').drawImage(img, 0, 0, img.width, img.height,
        x, y, img.width * ratio, img.height * ratio);
}

document.querySelector('video').addEventListener('play', function() {
    document.querySelector('#takePhotoButton').disabled = false;
});
document.querySelector('#getUserMediaButton').addEventListener('click', onGetUserMediaButtonClick);
document.querySelector('#takePhotoButton').addEventListener('click', onTakePhotoButtonClick);


function setFaceData(faceData) {
    var result = JSON.parse(faceData.veriler.result);

    $('#faceId').text(result[0].faceId);
    $('#top').text("top: " + result[0].faceRectangle.top);
    $('#left').text("left: "+result[0].faceRectangle.left);
    $('#width').text("width: "+result[0].faceRectangle.width);
    $('#height').text("height: " + result[0].faceRectangle.height);

    $('#smile').text("smile: " + result[0].faceAttributes.smile);
    $('#pitch').text("pitch: " + result[0].faceAttributes.headPose.pitch);
    $('#roll').text("roll: " + result[0].faceAttributes.headPose.roll);
    $('#yaw').text("yaw: " + result[0].faceAttributes.headPose.yaw);
    
    $('#gender').text("gender: " + result[0].faceAttributes.gender);
    $('#age').text("age: " + result[0].faceAttributes.age);
    $('#moustache').text("moustache: " + result[0].faceAttributes.facialHair.moustache);
    $('#beard').text("beard: " + result[0].faceAttributes.facialHair.beard);
    $('#sideburns').text("sideburns: " + result[0].faceAttributes.facialHair.sideburns);

    $('#glasses').text("glasses: " + result[0].faceAttributes.glasses);

    $('#anger').text("anger: " + result[0].faceAttributes.emotion.anger);
    $('#contempt').text("contempt: " + result[0].faceAttributes.emotion.contempt);
    $('#disgust').text("disgust: " + result[0].faceAttributes.emotion.disgust);
    $('#fear').text("fear: " + result[0].faceAttributes.emotion.fear);
    $('#happiness').text("happiness: " + result[0].faceAttributes.emotion.happiness);
    $('#neutral').text("neutral: " + result[0].faceAttributes.emotion.neutral);
    $('#sadness').text("sadness: " + result[0].faceAttributes.emotion.sadness);
    $('#surprise').text("surprise: " + result[0].faceAttributes.emotion.anger);

    $('#blurLevel').text("blurLevel: " + result[0].faceAttributes.blur.blurLevel);
    $('#blurValue').text("blurValue: " + result[0].faceAttributes.blur.value);

    $('#exposureLevel').text("exposureLevel: " + result[0].faceAttributes.exposure.exposureLevel);
    $('#exposureValue').text("exposureValue: " + result[0].faceAttributes.exposure.value);

    $('#noiseLevel').text("noiseLevel: " + result[0].faceAttributes.noise.noiseLevel);
    $('#noiseValue').text("noiseValue: " + result[0].faceAttributes.noise.value);

    $('#eyeMakeup').text("eyeMakeup: " + result[0].faceAttributes.makeup.eyeMakeup);
    $('#lipMakeup').text("lipMakeup: " + result[0].faceAttributes.makeup.lipMakeup);

    $('#accessories').text("accessories: " + result[0].faceAttributes.accessories);


    $('#foreheadOccluded').text("foreheadOccluded: " + result[0].faceAttributes.occlusion.foreheadOccluded);
    $('#eyeOccluded').text("eyeOccluded: " + result[0].faceAttributes.occlusion.eyeOccluded);
    $('#mouthOccluded').text("mouthOccluded: " + result[0].faceAttributes.occlusion.mouthOccluded);

    $('#bald').text("bald: " + result[0].faceAttributes.hair.bald);
    $('#invisible').text("invisible: " + result[0].faceAttributes.hair.invisible);

    $('#hairColorBox').html("");
    $.each(result[0].faceAttributes.hair.hairColor, function (index, value) {
        $('#hairColorBox').append('<p class="list-group-item-text"><strong>' + value.color + ":</strong> " + value.confidence+'</p>' )
    });
}

function setEmotionData(emotionData) {
    var result = JSON.parse(emotionData.veriler.result);

    $('#angerE').html("<strong>Anger:</strong> " + result[0].scores.anger);
    $('#contemptE').html("<strong>Contempt:</strong> " + result[0].scores.contempt);
    $('#disgustE').html("<strong>Disgust:</strong> " + result[0].scores.disgust);
    $('#fearE').html("<strong>Fear:</strong> " + result[0].scores.fear);
    $('#happinessE').html("<strong>Happiness:</strong> " + result[0].scores.happiness);
    $('#neutralE').html("<strong>Neutral:</strong> " + result[0].scores.neutral);
    $('#sadnessE').html("<strong>Sadness:</strong> " + result[0].scores.sadness);
    $('#surpriseE').html("<strong>Surprise:</strong> " + result[0].scores.surprise);



}