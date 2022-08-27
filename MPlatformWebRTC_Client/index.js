const videoContainer = document.getElementById("videoContainer");

function connect() {
    var peerId = "Room410";
    var webrtc = new SimpleWebRTC({
        target: "Streamer210",
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
        detectSpeakingEvents: true,
        autoAdjustMic: false,
    });

    webrtc.joinRoom("Room410");
    webrtc.on("videoAdded", function (video, peer) {
        console.log("video added", peer);
        var container = document.getElementById("videoContainer");

        video.setAttribute('loop', '');
        video.setAttribute('autoplay', 'true');
        video.setAttribute('controls', '');
        video.setAttribute('width', '100%');
        video.setAttribute('height', '100%');

        videoEl = video;
        container.innerHTML = "";
        container.appendChild(video);
        webrtc.stopLocalVideo();
        video.play();
    });

    webrtc.on("videoRemoved", function (video, peer) {
        console.log("video removed ", peer);
        var container = document.getElementById("videoContainer");
        if ( peer.id == peerId || peer.strongId == peerId || peer.nickName == peerId ) {
            videoEl = null;
            container.innerHTML = "";
            var videoStub = document.createElement("video");
            container.appendChild(videoStub);
        }
    });
}