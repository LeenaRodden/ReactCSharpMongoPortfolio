import Animation from './Animation';
import React, { useState } from 'react';
import { server } from './constants';
import { ContactInfo } from './ContactInfo';

export interface ISettings {
    Name: string,
    Description: string,
    PhoneNumber: string,
    SummaryText: string
}


export function Home() {

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

        return (
            <div>
                Loading
            </div>
            )
    }

    return (
        <div className='homeMain'>
            <Animation Name={settings.Name} Description={settings.Description} PhoneNumber={settings.PhoneNumber} SummaryText={settings.SummaryText } />
            <hr />
            <ContactInfo />
        </div>
        )
}