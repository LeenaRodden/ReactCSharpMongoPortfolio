import React, { useState } from 'react';
import { server } from './constants';

interface IContactInfo {
    ContactInfoId: number,
    Type: string,
    Details: string,
    DisplayOnHome: boolean
}

export function ContactInfo() {
    const [contactInfo, setContactInfo] = useState<IContactInfo[] | null>(null);

    let suffix = '';
    if (server.lastIndexOf('/data') > -1) {
        suffix = '.json';
    }

    if (contactInfo === null) {
        fetch(server + '/contactinfo' + suffix)
            .then(response => response.json())
            .then(data => processContactInfo(data))
            .catch(err => alert(err))

        return (<div>Loading</div>);
    }

    return (
        <div className='container contactInfo'>
            {contactInfo.map((p: IContactInfo) =>
                <div className='row' key={p.ContactInfoId}>
                    {p.Type}: <MakeLink contact={p} />
                </div>
            )}
        </div>
        )

    function processContactInfo(data: IContactInfo[]) {
        let infoToDisplay:IContactInfo[] = [];
        for (let i = 0; i < data.length; i++) {
            if (data[i].DisplayOnHome) {
                infoToDisplay.push(data[i]);
            }
        }
        setContactInfo(infoToDisplay);
    }

    interface IMakeLink {
        contact: IContactInfo
    }

    function MakeLink(props: IMakeLink) {
        if (props.contact.Type.toLowerCase() === 'email') {
            return(<a href={'mailto://' + props.contact.Details}>{props.contact.Details}</a>);
        } else if (props.contact.Details.indexOf('http') === 0) {
            return(<a href={props.contact.Details}>{props.contact.Details}</a>);
        } else {
            return(<span>{props.contact.Details}</span>)
        }
    }

}