import { Navigate } from "react-router-dom";
import { appState } from "../store/AppState";

/**
 * Wrapper that defines whether a route requires authentication.
 */
function Authenticated( { children } : { children : JSX.Element } )
{
    console.log(`User is authenticated? ${appState.authenticated}`);

    return appState.authenticated ? children : <Navigate to="/auth" replace />
}

export default Authenticated