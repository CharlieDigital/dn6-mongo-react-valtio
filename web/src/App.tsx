import { Route, Routes, useNavigate } from "react-router-dom";
import "./App.css";
import LoginView from "./components/LoginView";
import Authenticated from "./components/Authenticated";
import MainGrid from "./components/MainGrid";
import { useEffect } from "react";
import { appState } from "./store/AppState";
import { Auth } from "aws-amplify";
import { CognitoUser } from "@aws-amplify/auth";

// The main app UI.
function App()
{
    const navigate = useNavigate();

    // If the user is authenticated, update route.
    useEffect(() =>
    {
        async function initAuth()
        {
            try
            {
                const user: CognitoUser = await Auth.currentAuthenticatedUser();
                appState.authenticated = user ? true : false;

                const session = await Auth.currentSession();
                OpenAPI.TOKEN = session.getIdToken().getJwtToken();

                navigate("/");
            }
            catch
            {
                // User is not authenticated
                appState.authenticated = false;
            }
        }

        initAuth();
    }, []);

    return (
        <div>
            <Routes>
                <Route path="/" element=
                {
                    <Authenticated>
                        <MainGrid />
                    </Authenticated>
                }
                />
                <Route path="auth" element={ <LoginView /> } />
            </Routes>
        </div>
    );
}

export default App;
