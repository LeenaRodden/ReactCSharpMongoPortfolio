import React, { useState } from 'react';
import { server } from './constants';

export function Experience() {
    interface IExperience {
        ExperienceId: number;
        Dates: string;
        JobTitle: string;
        Company: string;
        Location: string;
        BulletPoints: string[];
    }
    const [experience, setExperience] = useState<IExperience[] | null>(null);

    let suffix = "";
    if (server.lastIndexOf('/data') > -1) {
        suffix = ".json";
    }

    if (experience === null) {
        fetch(server + "/experience" + suffix)
            .then(response => response.json())
            .then(data => processExperience(data))
            .catch(err => alert(err))
    }

    return (
        <div>
            <h2>Experience</h2>
            <hr />
            <div id='divExperience'>
                <ExperienceList />
            </div>
        </div>
    );

    function ExperienceList() {
        if (experience === null) {
            return (<h3>Loading...Please Wait</h3>);
        }
        return (
            <div className='container'>
                {experience.map((p: IExperience) =>
                    <div className='row experienceRow' key={p.ExperienceId}>
                        <h3>{p.JobTitle}</h3>
                        <div>{p.Dates}</div>
                        <span>{p.Company}</span><div className='experienceLocation'>{p.Location}</div>
                        <div><ExperienceBulletPoints data={p.BulletPoints} id={p.ExperienceId} /></div>
                    </div>
                )}
            </div>
        );
    }

    interface IExpBP {
        data: string[];
        id: number;
    }

    function ExperienceBulletPoints(props: IExpBP) {
        if (props.data == null) {
            return (<div>???</div>)
        }


        return (
            <ul key={props.id + "ul"}>
                {props.data.map((p: any) =>
                    <li key={p}>
                        {p}
                    </li>
                )}
            </ul>
            );
    }

    function processExperience(data: IExperience[]) {
        setExperience(data);
    }

}