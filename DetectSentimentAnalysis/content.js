chrome.runtime.onMessage.addListener(function (request, sender, sendResponse) {
  var selectedText = window
    .getSelection()
    .toString()
    .trim()
    .replace(/\r?\n|\r/g, "");
  //const apiUrl = "https://localhost:7243/api/GetSentiment";
  // fetchOptions = {
  //   method: "POST",
  //   headers: {
  //     "Content-Type": "application/json",
  //   },
  //   body: JSON.stringify({ text: selectedText }),
  // };
  // const response =  fetch(apiUrl, fetchOptions)
  //   .then((response) => {
  //     if (!response.ok) {
  //       throw new Error('HTTP error! Status: ${response.status}');
  //     }
  //   })
  //   .then((data) => {
  //     console.log(data);
  //     const setnimentAnalysis = data.map((sentiment) => ({
  //       sentiment: sentiment.sentiment,
  //       pii: sentiment.pii,
  //       piiInfo: sentiment.piiInfo,
  //     }));
  //     chrome.storage.local.set(setnimentAnalysis);
  //   })
  //   .catch((error) => console.error("Error:", error));
  // alert(response);
  sendResponse(selectedText);
});
