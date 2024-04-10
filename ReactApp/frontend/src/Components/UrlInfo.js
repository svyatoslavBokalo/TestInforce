import { Button } from 'bootstrap';
import React from 'react'
import './css/styles.css'

class UrlInfo extends React.Component{
    constructor(props){
        super(props);
        this.state = {
          urlInfos:[]
        }
        this.API_URL = "https://localhost:7253/";
    }

    componentDidMount(){
        this.refreshUrlInfos();
    }

    // handleClick = () => {
    //     const { history } = this.props;
    //     history.push('/login');
    //   }

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

    render(){
        const{urlInfos} = this.state;
        return (
            <div>
                <header>

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

export default UrlInfo;