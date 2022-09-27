import React, { useState } from 'react';
import { server } from './constants';

export function Education() {
    interface IEducation {
        EducationId: number;
        GraduationDate: string;
        DegreeText: string;
        University: string;
        Location: string;
        Notes: string;
    }
    const [education, setEducation] = useState<IEducation [] | null>(null);

    let suffix = '';
    if (server.lastIndexOf('/data') > -1) {
        suffix = '.json';
    }

    if (education === null) {
        fetch(server + '/education' + suffix)
            .then(response => response.json())
            .then(data => processEducation(data))
            .catch(err => alert(err))
    }

    return (
        <div>
            <h2>Education</h2>
            <hr />
            <div id='divEducation'>
                <EducationList />
            </div>
        </div>
    );

    function EducationList() {
        if (education === null) {
            return (<h3>Loading...Please Wait</h3>);
        }
        return (
            <div className='container'>
                {education.map((p:IEducation) =>
                    <div className='row experienceRow' key={p.EducationId}>
                        <h3>{p.DegreeText}</h3>
                        <p>{p.GraduationDate}</p>
                        <p>{p.University}</p>
                        <p>{p.Location}</p>
                        <p>{p.Notes}</p>
                    </div>
                )}
            </div>
            );
    }

    function processEducation(data: IEducation[]) {
        setEducation(data);
    }
}