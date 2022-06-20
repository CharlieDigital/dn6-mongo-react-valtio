import "../App.css";
import { Button, Card, CardContent, Grid, Input, Table, TableBody, TableCell, TableHead, TableRow } from "@mui/material";
import Add from "@mui/icons-material/Add";
import Logout from "@mui/icons-material/Logout";
import { useSnapshot } from "valtio";
import { useEffect, useState } from "react";
import { appState } from "../store/AppState";
import CompanyRow from "./CompanyRow";
import { Auth } from "aws-amplify";
import { useNavigate } from "react-router-dom";

/**
 * The main grid in the UI.
 */
function MainGrid() {
  const navigate = useNavigate();

  var [group, setGroup] = useState("");
  var [message, setMessage] = useState("");

  // Add a company to the global state.
  async function addCompany() {
    await appState.addRandomCompany();
  }

  // Performs the logout
  async function logOut() {
    await Auth.signOut();

    navigate("/auth");
  }

  // Broadcast a message.
  async function broadcast() {
    await appState.broadcast();
  }

  // Joins a user to a group.
  async function joinGroup() {
    await appState.joinGroup(group);
  }

  // Sends a message to the specified group.
  async function messageGroup() {
    await appState.notifyGroup(
      group,
      message
    );
  }

  const { companies } = useSnapshot(appState);

  // Trigger on the initial load.
  useEffect(() => {
    (async () => await appState.loadCompanies())()
      .catch(console.log);
  }, [""]);

  return (
    <Grid container spacing={3}>
      <Grid item sm={0} md={1} lg={2}>

      </Grid>
      <Grid item sm={12} md={10} lg={8}>
        <Card
          sx={{ mx: 1, my: 4 }}
          variant="outlined">
          <CardContent>
            This app resets every 2 hours.  See the OpenAPI spec <a href={`${import.meta.env.VITE_API_ENDPOINT}/swagger/index.html`}>here</a>.
            <Button
              sx={{ ml: 4 }}
              variant="outlined"
              size="small"
              startIcon={<Logout />}
              onClick={async () => await logOut()}>
              Logout
            </Button>
          </CardContent>
          <CardContent>
            Click the button to add a randomly generated Company.
            <Button
              sx={{ ml: 4 }}
              variant="outlined"
              size="small"
              startIcon={<Add />}
              onClick={async () => await addCompany()}
              disabled={companies ? companies.length >= 10 : false}>
              Add Company
            </Button>
          </CardContent>
          <CardContent>
            Click the button to broadcast a notification.
            <Button
              sx={{ ml: 4 }}
              variant="outlined"
              size="small"
              startIcon={<Add />}
              onClick={async () => await broadcast()}>
              Broadcast
            </Button>
          </CardContent>
          <CardContent>
            <p>Click the button to join a group</p>
            <div>
              <Input onChange={(e) => setGroup(e.target.value)}/>
              <Button
                sx={{ ml: 4 }}
                variant="outlined"
                size="small"
                startIcon={<Add />}
                onClick={async () => await joinGroup()}>
                Join
              </Button>
            </div>
            <p>Click the button to send a message only to the group.</p>
            <div>
              <Input onChange={(e) => setMessage(e.target.value)}/>
              <Button
                sx={{ ml: 4 }}
                variant="outlined"
                size="small"
                startIcon={<Add />}
                onClick={async () => await messageGroup()}>
                Send
              </Button>
            </div>
          </CardContent>
          <CardContent>
            Showing {companies.length} companies (max 10; delete or reset to restart).  Total comp: <strong>${appState.totalCompensation}</strong>

            <Table size="small">
              <TableHead>
                <TableRow>
                  <TableCell>+</TableCell>
                  <TableCell>Company</TableCell>
                  <TableCell>ID</TableCell>
                  <TableCell>Delete</TableCell>
                </TableRow>
              </TableHead>
              <TableBody>
                {
                  companies.map((company) =>
                  (
                    <CompanyRow key={company.id} company={company} />
                  ))
                }
              </TableBody>
            </Table>
          </CardContent>
        </Card>
      </Grid>
      <Grid item sm={0} md={1} lg={2}>

      </Grid>
    </Grid>
  );
}

export default MainGrid;