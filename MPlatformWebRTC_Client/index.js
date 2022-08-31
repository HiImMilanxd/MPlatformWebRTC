const videoContainer = document.getElementById("videoContainer");

var WebRTCOutput = new SimpleWebRTC({
    target: "Streamer0",
    url: "http://rtc.medialooks.com:8889",
    iceServers: [
        { urls: "stun:stun.l.google.com:19302" },
        {
            username: "test_user",
            credential: "medialooks",
            urls: ["turn:67.220.183.67:3478"],
        },
    ],
    localVideoEl: "",
    remoteVideosEl: "",
    autoRequestMedia: false,
    debug: true,
    muted : true,
    detectSpeakingEvents: true,
    autoAdjustMic: false,
});

var WebRTCPreview1 = new SimpleWebRTC({
    target: "Streamer1",
    url: "http://rtc.medialooks.com:8889",
    iceServers: [
        { urls: "stun:stun.l.google.com:19302" },
        {
            username: "test_user",
            credential: "medialooks",
            urls: ["turn:67.220.183.67:3478"],
        },
    ],
    localVideoEl: "",
    remoteVideosEl: "",
    autoRequestMedia: false,
    debug: true,
    mute : true,
    detectSpeakingEvents: true,
    autoAdjustMic: false,
});

var WebRTCPreview2 = new SimpleWebRTC({
    target: "Streamer2",
    url: "http://rtc.medialooks.com:8889",
    iceServers: [
        { urls: "stun:stun.l.google.com:19302" },
        {
            username: "test_user",
            credential: "medialooks",
            urls: ["turn:67.220.183.67:3478"],
        },
    ],
    localVideoEl: "",
    remoteVideosEl: "",
    autoRequestMedia: false,
    debug: true,
    mute : true,
    detectSpeakingEvents: true,
    autoAdjustMic: false,
});

var communicationChannal = new SimpleWebRTC({
    target: "Streamer999",
    url: "http://rtc.medialooks.com:8889",
    iceServers: [
        { urls: "stun:stun.l.google.com:19302" },
        {
            username: "test_user",
            credential: "medialooks",
            urls: ["turn:67.220.183.67:3478"],
        },
    ],
    localVideoEl: "",
    remoteVideosEl: "",
    autoRequestMedia: false,
    debug: true,
    mute : true,
    detectSpeakingEvents: true,
    autoAdjustMic: false,
});

function connect() {
    
    WebRTCOutput.joinRoom("Room410");
    WebRTCOutput.on("videoAdded", function (video, peer) {
        console.log("video added", peer);
        var container = document.getElementById("videoContainerOutput");

        video.setAttribute('loop', '');
        video.setAttribute('autoplay', 'true');
        video.setAttribute('controls', '');
        video.setAttribute("width", "720px");

        videoEl = video;
        container.innerHTML = "";
        container.appendChild(video);
        WebRTCOutput.stopLocalVideo();
        video.play();
    });  

    WebRTCPreview1.joinRoom("Room410");
    WebRTCPreview1.on("videoAdded", function (video, peer) {
        console.log("video added", peer);
        var container = document.getElementById("videoContainer1");

        video.setAttribute('loop', '');
        video.setAttribute('autoplay', 'true');
        video.setAttribute('controls', '');
        video.setAttribute("width", "480px");

        videoEl = video;
        container.innerHTML = "";
        container.appendChild(video);
        WebRTCPreview1.stopLocalVideo();
        video.play();
    });

    WebRTCPreview2.joinRoom("Room410");
    WebRTCPreview2.on("videoAdded", function (video, peer) {
        console.log("video added", peer);
        var container = document.getElementById("videoContainer2");

        video.setAttribute('loop', '');
        video.setAttribute('autoplay', 'true');
        video.setAttribute('controls', '');
        video.setAttribute("width", "480px");

        videoEl = video;
        container.innerHTML = "";
        container.appendChild(video);
        WebRTCPreview2.stopLocalVideo();
        video.play();
    });

    communicationChannal.joinRoom("Room410");
}

function switchsrc(srcID){
    communicationChannal.sendDataChannelMessageToAll(srcID);
}