import React, { useState } from 'react';
import { server } from './constants';
import { Education } from "./Education";
import { Experience } from "./Experience";
import { Skills } from "./Skills";
import { ISettings } from "./Home";

export function Overview() {
    const [settings, setSettings] = useState<ISettings | null>(null);

    let suffix = "";
    if (server.lastIndexOf('/data') > -1) {
        suffix = ".json";
    }

    if (settings === null) {
        fetch(server + "/settings" + suffix)
            .then(response => response.json())
            .then(data => setSettings(data))
            .catch(err => alert(err))

    }

    return (<div>
        <h2>Summary</h2>
        <hr />
        <div className='container'>
        <p className='summaryText'>
                {settings === null ? <h2>Loading</h2> : <span>{settings.SummaryText}</span> }
        </p>
         </div>
        <Skills />
        <Experience />
        <Education />
    </div>);
}