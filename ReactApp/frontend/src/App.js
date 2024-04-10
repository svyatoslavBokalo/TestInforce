import React from 'react';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import UrlInfo from './Components/UrlInfo';
import CreateUrlInfo from './Components/CreateUrlInfo';

// function App() {
//   return (
//     <BrowserRouter>
//       <Routes>
//           <Route path="/" component={UrlInfo} />
//           <Route path="/info" element={CreateUrlInfo} />
//       </Routes>
//     </BrowserRouter>
//   );
// }
class App extends React.Component{
  constructor(props){
    super(props);
    this.state = {
      urlInfos:[],
      linkUrl: ''
    }
    this.API_URL = "https://localhost:7253/";
}

componentDidMount(){
    this.refreshUrlInfos();
}

// async handleInfoClick(url) {
//     // Встановлюємо стан для перенаправлення на сторінку інформації
//     this.setState({ redirectToInfoPage: true, infoPageUrl: url });
// }

// handleRegisterClick = () => {
//     this.navigate("/register");
// }
handleInputChange = (event) => {
  this.setState({ linkUrl: event.target.value });
}

async refreshUrlInfos() {
    try {
        const response = await fetch(this.API_URL + "api/UrlInfo/GetAllUrl");
        if (!response.ok) {
            throw new Error('Failed to fetch data');
        }
        const data = await response.json();
        this.setState({ urlInfos: data });
    } catch (error) {
        console.error('Error fetching data:', error);
    }
}

async deleteUrl(id) {
    try {
        const response = await fetch(`${this.API_URL}api/UrlInfo/${id}`, {
            method: 'DELETE'
        });

        if (!response.ok) {
            throw new Error('Failed to delete URL');
        }

        // Оновити стан після успішного видалення URL
        this.refreshUrlInfos();
    } catch (error) {
        console.error('Error deleting URL:', error);
    }
}

async AddUrl(url, user) {
  try {
      const response = await fetch(`${this.API_URL}api/UrlInfo/GetUrl${url}, ${user}`, {
          method: 'POST'
      });

      if (!response.ok) {
          throw new Error('Failed to delete URL');
      }

      // Оновити стан після успішного видалення URL
      this.refreshUrlInfos();
  } catch (error) {
      console.error('Error deleting URL:', error);
  }
}

AddUrlWithURL = () => {
  const { linkUrl } = this.state;
  // Виклик функції AddUrl з отриманим значенням поля вводу
  this.AddUrl(linkUrl, "2323124");
}

render(){
    const{urlInfos } = this.state;
    return (
        <div>
            <header>
                <input id="link_url" placeholder='input url' onChange={this.handleInputChange}></input>
                <button onClick={() => this.AddUrlWithURL}>Add</button>
            </header>
            <table>
                <thead>
                    <tr>
                        <th>URL</th>
                        <th>ShortUrl</th>
                        <th>Info</th>
                        <th>Delete</th>
                    </tr>
                </thead>
                <tbody>
                    {urlInfos.map(urlInfo =>
                        <tr key={urlInfo.ID}>
                            <td>{urlInfo.UrlString}</td>
                            <td><a href={urlInfo.UrlString}>{urlInfo.ShortUrl}</a></td>
                            <td>
                                {/* <button onClick={this.handleClick}>Go to Login</button> */}
                            </td>
                            <td>
                                <button onClick={() => this.deleteUrl(urlInfo.ID)}>Delete</button>
                            </td>
                        </tr>
                    )}
                </tbody>
            </table>
        </div>
    );
}
}

export default App;
