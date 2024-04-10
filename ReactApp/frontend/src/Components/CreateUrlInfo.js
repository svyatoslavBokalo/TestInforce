//import React, { Component } from 'react';

// class ShortUrlInfoPage extends Component {
//     render() {
//         // Отримайте інформацію про ShortUrlInfo з props
//         const { shortUrlInfo } = this.props;

//         return (
//             <div>
//                 <h2>Short URL Information</h2>
//                 <p>URL: {shortUrlInfo.UrlString}</p>
//                 <p>Short URL: {shortUrlInfo.ShortUrl}</p>
//                 <p>Title: {shortUrlInfo.Title}</p>
//                 <p>Description: {shortUrlInfo.Description}</p>
//                 <p>Created Date: {shortUrlInfo.CreatedDate}</p>
//                 <p>Created By: {shortUrlInfo.CreatedBy}</p>
//             </div>
//         );
//     }
// }

// export default ShortUrlInfoPage;

import React from 'react';

// const CreateUrlInfo = (urlString) => {
//     const urlInfo = {
//         Id: null,
//         UrlString: urlString,
//         ShortUrl: null,
//         Title: null,
//         Description: null,
//         CreatedDate: new Date().toISOString(), // Поточна дата та час
//         CreatedBy: "YourName" // Замініть "YourName" на потрібну вам назву користувача
//       };
//     const refreshUrlInfo = async () => {
//         try {
//             const response = await fetch(this.API_URL + "api/UrlInfo/GetUrl");
//             if (!response.ok) {
//                 throw new Error('Failed to fetch data');
//             }
//             const data = await response.json();
//             this.setState({ urlInfo: data });
//         } catch (error) {
//             console.error('Error fetching data:', error);
//         }
//     };

//   return urlInfo;
// };

function CreateUrlInfo(urlString){
    const urlInfo = {
        Id: null,
        UrlString: urlString,
        ShortUrl: null,
        Title: null,
        Description: null,
        CreatedDate: new Date().toISOString(), // Поточна дата та час
        CreatedBy: "YourName" // Замініть "YourName" на потрібну вам назву користувача
      };
    const refreshUrlInfo = async () => {
        try {
            const response = await fetch(this.API_URL + "api/UrlInfo/GetUrl");
            if (!response.ok) {
                throw new Error('Failed to fetch data');
            }
            const data = await response.json();
            this.setState({ urlInfo: data });
        } catch (error) {
            console.error('Error fetching data:', error);
        }
    };

  return (<div>
    <h3>{urlInfo.Id}</h3>
    <h3>{urlInfo.UrlString}</h3>
    <h3>{urlInfo.ShortUrl}</h3>
    <h3>{urlInfo.Title}</h3>
    <h3>{urlInfo.Description}</h3>
    <h3>{urlInfo.CreatedDate}</h3>
    <h3>{urlInfo.CreatedBy}</h3>
  </div>);
};

export default CreateUrlInfo;