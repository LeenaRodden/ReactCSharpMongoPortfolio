import React, { useState } from 'react';
import { server } from './constants';

export interface ISkills {
    TechnologyId: number;
    TechnologyName: string;
    TechnologyType: string;
}

export function Skills() {

    const [skills, setSkills] = useState<ISkills[] | null>(null);

    let suffix = "";
    if (server.lastIndexOf('/data') > -1) {
        suffix = ".json";
    }

    if (skills === null) {
        fetch(server + "/technology" + suffix)
            .then(response => response.json())
            .then(data => processSkills(data))
            .catch(err => alert(err))
    }

    return (
        <div>
            <h2>Skills</h2>
            <hr />
            <div id='divSkills'>
                <SkillSet />
            </div>
        </div>
    );

    function SkillSet() {
        if (skills === null) {
            return (<h3>Loading...Please Wait</h3>);
        }

        let arrSkillTypes: string[] = [];
        for (let i = 0; i < skills.length; i++) {
            if (!arrSkillTypes.includes(skills[i].TechnologyType)) {
                arrSkillTypes.push(skills[i].TechnologyType);
            }
        }

        return (
            <div className='container'>
                {arrSkillTypes.map((p: string) =>
                    <div className='row experienceRow' key={p}>
                        <h3>{p}</h3>
                        <p><TechNames techType={p} /></p>
                    </div>
                )}
            </div>
            );
    }

    interface TechNameProps {
        techType: string;
    }

    function TechNames(props:TechNameProps) {
        let csv = '';
        if (skills === null) {
            return (<span>{csv}</span>);
        }
        for (let i = 0; i < skills.length; i++) {
            if (skills[i].TechnologyType === props.techType) {
                if (csv !== '') {
                    csv += ', ';
                }
                csv += skills[i].TechnologyName;
            } 
        }
        return (<span>{csv}</span>);
    }

    function processSkills(data: ISkills[]) {
        setSkills(data);
    }
}