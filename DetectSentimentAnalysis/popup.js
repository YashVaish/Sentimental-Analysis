document.addEventListener("DOMContentLoaded", function () {
  var analyzeButton = document.getElementById("analyzeButton");

  analyzeButton.addEventListener("click", function () {
    // chrome.runtime.sendMessage({message:"Basic functionality is working"},(response)=>{
    //   console.log(response.message);
    // })
    chrome.tabs.query({ active: true, currentWindow: true }, function (tabs) {
      var activeTab = tabs[0];
      chrome.tabs.sendMessage(activeTab.id, { action: "analyzeSentiment" },(response)=>{
        alert(response);

        const apiUrl = "https://localhost:7243/api/GetSentiment";
        fetchOptions = {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
            "Access-Control-Allow-Origin": "*",
            "Accept":"application/json"
          },
          body: JSON.stringify({ text: response }),
        };
        const apiresponse = fetch(apiUrl, fetchOptions)
          .then((res) => {
            if (!res.ok) {
              throw new Error("HTTP error! Status: ${response.status}");
            }
            else{
              alert (res.statuscode);
            }
          })
          .then((res) => {
            console.log(res);
            const resultText = document.getElementById("resultText");
            resultText.textContent = JSON.stringify(res);
          })
          .catch((error) => console.error("Error:", error));
        alert(res);
      });
      alert(apiresponse);
      });
      // chrome.storage.get(["sentiment", "pii", "piiInfo"]),
      //   (result) => {
      //     const { sentiment, pii, piiInfo } = response;
      //     console.log(response);
      //   };

      // document.getElementById('sentiment').innerText(response.sentiment);
      // document.getElementById('pii').innerText(response.pii);
      // document.getElementById('piiInfo').innerText(response.piiInfo);
      
      // const dialog = document.querySelector("dialog");
      // dialog.showModal();
    });
  });
