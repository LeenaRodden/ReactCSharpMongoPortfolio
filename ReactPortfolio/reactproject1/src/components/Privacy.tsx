import React, { useState } from 'react';

export function Privacy() {
    const [value, setValue] = useState<number | null>(null);

    function closeMessage() {
        setCookie("cookiesAccepted", '1', 7);
        setValue(1);
    }

    if (value == null) {
        let cookie = getCookie("cookiesAccepted");
        if (cookie === "1") {
            setValue(1);
        } else {
            setValue(0);
        }
    }

    if (value == 0) {
        return (
            <div className="privacyWarning">
                <h5>
                    This website may use cookies to provide a better user experience.
                </h5>
                <p>
                    By using this website you are consenting to the use of cookies.
                </p>
                <input type="button" value="Close" className="closeButton" onClick={closeMessage} />
            </div>
        )
    } else {
        return (<span></span>)
    }

    function setCookie(name:string, value:string, days:number) {
        const d = new Date();
        d.setTime(d.getTime() + (days * 24 * 60 * 60 * 1000));
        let expires = "expires=" + d.toUTCString();
        document.cookie = name + "=" + value + ";" + expires + ";path=/";
    }

    function getCookie(cookiename:string) {
        let name = cookiename + "=";
        let decodedCookie = decodeURIComponent(document.cookie);
        let ca = decodedCookie.split(';');
        for (let i = 0; i < ca.length; i++) {
            let c = ca[i];
            while (c.charAt(0) == ' ') {
                c = c.substring(1);
            }
            if (c.indexOf(name) == 0) {
                return c.substring(name.length, c.length);
            }
        }
        return "";
    }

}