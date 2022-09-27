import React, { useState } from 'react';
import { server } from './constants';
import { ISkills } from './Skills';

export function Projects() {
    interface IProjects {
        WebsiteId: number;
        Url: string;
        DisplayName: string;
        Description: string;
        Technologies: ISkills[];
        Images: object;
    }
    const [portfolio, setPortfolio] = useState<IProjects[] | null>(null);

    let suffix = "";
    if (server.lastIndexOf('/data') > -1) {
        suffix = ".json";
    }

    if (portfolio === null) {
        fetch(server + "/website" + suffix)
            .then(response => response.json())
            .then(data => processPortfolio(data))
            .catch(err => alert(err))
    }



    return (
        <div>
            <h2>Projects</h2>
            <hr />
            <div id='divProjects' className='container'>
                <ProjectsList />
            </div>
        </div>
    );

    function ProjectsList() {
        if (portfolio === null) {
            return (<h3>Loading...Please Wait</h3>);
        }
        let arr = [];
        let lastI = 0;
        for (let i = 0; i < portfolio.length; i++) {
            arr.push(<h2 key={i + 'h2'}>{portfolio[i].DisplayName}</h2>);
            arr.push(<a key={i + 'a'} href={portfolio[i].Url}>{portfolio[i].Url}</a>);
            arr.push(<p key={i + 'p'}>{portfolio[i].Description}</p>);
            /*            lastI = i;*/
            for (let j = 0; j < portfolio[i].Technologies.length; j++) {
                if (j == 0) {
                    arr.push(<h4 key={i + 'h4'}>Technologies used:</h4>)
                }
                arr.push(<span className='projectTech' key={i + 'tech' + j}>{portfolio[i].Technologies[j].TechnologyName}</span>)
            }
        }

        return (
            <div className='row experienceRow'>
                {arr}
                {/*<h5>{lastI}</h5>*/}
                {/*<code>{JSON.stringify(portfolio)}</code>*/}
            </div>
        )

    }

    function processPortfolio(data:IProjects[]) {
        setPortfolio(data);
    }
}