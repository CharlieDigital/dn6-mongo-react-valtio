import { Button, Card, CardContent, Grid } from "@mui/material";
import Google from "@mui/icons-material/Google";
import { Auth } from "aws-amplify";
import { CognitoHostedUIIdentityProvider } from '@aws-amplify/auth/lib/types'

/**
 * The view for authentication.
 */
function LoginView()
{
    function handleLogin()
    {
        Auth.federatedSignIn(
        {
            provider: CognitoHostedUIIdentityProvider.Google
        });
    }

    return (
        <Grid container spacing={ 3 }>
            <Grid item sm={ 0 } md={ 3 } lg={ 4 }>

            </Grid>
            <Grid item sm={ 12 } md={ 6 } lg={ 4 }>
                <Card
                    sx={{ mx: 1, my: 4 }}
                    variant="outlined">
                    <CardContent>
                        Login with Google
                        <Button
                            sx={{ ml: 4 }}
                            variant="outlined"
                            size="small"
                            startIcon={ <Google/> }
                            onClick={ handleLogin }>
                            Google
                        </Button>
                    </CardContent>
                </Card>
            </Grid>
            <Grid item sm={ 0 } md={ 3 } lg={ 4 }>

            </Grid>
        </Grid>
    );
}

export default LoginView;