import React, {useEffect } from 'react';
import logo from './logo.svg';
import './App.css';
import { Link, Route, Routes, BrowserRouter as Router } from 'react-router-dom';
import { publicPath } from './components/constants';
import { Home } from './components/Home';
import { Education } from './components/Education';
import { Experience } from './components/Experience';
import { Overview } from './components/Overview';
import { Projects } from './components/Projects';
import { Skills } from './components/Skills';
import { Privacy } from './components/Privacy';


function App() {
    const routeCodes = {
        HOME: publicPath,
        EDUCATION: `${publicPath}/education`,
        EXPERIENCE: `${publicPath}/experience`,
        OVERVIEW: `${publicPath}/overview`,
        PROJECTS: `${publicPath}/projects`,
        SKILLS: `${publicPath}/skills`,
    };

  return (
    <div className="App">
          <Router>
              <nav className="navbar navbar-inverse">
                  <div className="container-fluid">
                      <div className="navbar-header">
                          <button type="button" className="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
                              <span className="icon-bar"></span>
                              <span className="icon-bar"></span>
                              <span className="icon-bar"></span>
                          </button>
                          <a className="navbar-brand" href="#">Portfolio</a>
                      </div>
                      <div className="collapse navbar-collapse NOnavbar" id="myNavbar">
                          <ul className="nav navbar-nav">
                              <li role="presentation"><Link to={routeCodes.HOME} className="nav-link menuText" onClick={menuClicked}><span className="glyphicon glyphicon-home">
                              </span><span className='menubarText'>Home</span></Link></li>
                              <li role="presentation">
                                  <Link to={routeCodes.OVERVIEW} className="nav-link menuText" onClick={menuClicked}><span className="glyphicon glyphicon-plane">
                                  </span><span className='menubarText'>Overview</span></Link>
                              </li>
                              <li role="presentation">
                                  <Link to={routeCodes.EDUCATION} className="nav-link menuText" onClick={menuClicked} ><span className="glyphicon glyphicon-education">
                                  </span><span className='menubarText'>Education</span></Link></li>
                              <li role="presentation">
                                  <Link to={routeCodes.EXPERIENCE} className="nav-link menuText" onClick={menuClicked}><span className="glyphicon glyphicon-wrench">
                                  </span><span className='menubarText'>Experience</span></Link>
                              </li>
                              <li role="presentation">
                                  <Link to={routeCodes.PROJECTS} className="nav-link menuText" onClick={menuClicked}><span className="glyphicon glyphicon-briefcase">
                                  </span><span className='menubarText'>Projects</span></Link>
                              </li>
                              <li role="presentation">
                                  <Link to={routeCodes.SKILLS} className="nav-link menuText" onClick={menuClicked}><span className="glyphicon glyphicon-equalizer">
                                  </span><span className='menubarText'>Skills</span></Link>
                              </li>
                              <li role="presentation">
                                  <span className="toggler">
                                      Dark Mode
                                      <label className="switch">Mode
                                          <input type="checkbox" id="chkModeToggle" name="chkModeToggle" onClick={ModeToggled} tabIndex={0} />
                                          <span className="slider round"></span>
                                      </label>
                                      Light Mode

                                  </span>
                              </li>
                          </ul>
                      </div>
                  </div>
              </nav>
              <Routes>
                  <Route path='/' element={<Home />} />
                  <Route path={routeCodes.HOME} element={<Home />} />
                  <Route path={routeCodes.EDUCATION} element={<Education />} />
                  <Route path={routeCodes.EXPERIENCE} element={<Experience />} />
                  <Route path={routeCodes.OVERVIEW} element={<Overview />} />
                  <Route path={routeCodes.PROJECTS} element={<Projects />} />
                  <Route path={routeCodes.SKILLS} element={<Skills />} />
                  <Route
                      path="*"
                      element={
                          <main style={{ padding: "1rem" }}>
                              <p>Not found</p>
                              <p className='warning'>If this page ever existed, it has since been deleted.</p>
                          </main>
                      }
                  />
              </Routes>
          </Router>
          <Privacy />
          <footer>Copyright 2022 Leena Rodden. All rights reserved.</footer>
    </div>
  );

    function menuClicked(e:any) {
        let myNavbar = document.getElementById("myNavbar");
        if (myNavbar != null &&  myNavbar.classList.contains("in")) {
            myNavbar.classList.remove("in");
        }

        var menuItems = document.querySelectorAll(".menuText");
        for (var i = 0; i < menuItems.length; i++) {
            if (menuItems[i].classList.contains("menuTextActive")) {
                menuItems[i].classList.remove("menuTextActive");
            }

        }
        e.target.classList.add("menuTextActive");
    }

    function ModeToggled(e:any) {
        let cssMode = document.getElementById("cssMode") as HTMLLinkElement;
        //let contentFolder = '<%:ResolveUrl("~/Content/") %>';
        
        let mode = getCookie("mode");
/*        useEffect(() => {*/
            if (cssMode != null) {
                let contentFolder = cssMode.href;
                contentFolder = contentFolder.replace('classicmode.css', '');
                contentFolder = contentFolder.replace('darkmodetemp.css', '');
                if (mode == "dark" || mode == "") {
                    document.cookie = "mode=classic; max-age=2592000; path=/";
                    cssMode.href = contentFolder + "classicmode.css";
                } else {
                    document.cookie = "mode=dark; max-age=2592000; path=/";
                    cssMode.href = contentFolder + "darkmodetemp.css";
                }
        }

/*        }, [mode]);*/
        menuClicked(e);
        return null;

    }

    function getCookie(cname: string) {
        let name = cname + "=";
        let ca = document.cookie.split(';');
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

export default App;
