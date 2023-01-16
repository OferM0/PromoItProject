import React, { useState, useEffect } from "react";
import { ManagerCampaign } from "../../components/campaign/managerCampaign";
import { getCampaigns } from "../../../services/campaign.service";
import "./managerCampaigns.css";
import { useAuth0 } from "@auth0/auth0-react";
import { getUsers } from "../../../services/user.service";
import { getProducts } from "../../../services/product.service";
// import {
//   getTweetsByUser,
//   getRetweetsOfUser,
// } from "../../../services/twitter.service";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import moment from "moment";
import { Dropdown, DropdownButton } from "react-bootstrap";

export const ManagerCampaignsPage = (props) => {
  const [campaignsArr, setCampaignsArr] = useState([]);
  const { user } = useAuth0();
  const [users, setUsers] = useState([]);
  //const [FilterUsers, setFilterUsers] = useState([]);
  const [FilterCampaigns, setFilterCampaigns] = useState([]);
  const [isActive, setIsActive] = useState("Active");
  const [boolActive, setBoolActive] = useState(true);
  const [products, setProducts] = useState([]);
  //const [tweetsAndRetweets, setTweetsAndRetweets] = useState([]);
  const [startDate, setStartDate] = useState("");
  const [endDate, setEndDate] = useState("");
  const [selected, setSelected] = useState("Sort By");
  const [searchText, setSearchText] = useState("");

  const handleActiveChange = (event) => {
    setIsActive(event.target.value);
    if (event.target.value === "Active") {
      setBoolActive(true);
    } else {
      setBoolActive(false);
    }
  };

  const fetchData = async () => {
    let response = await getUsers();
    if (response.status === 200) {
      setUsers(Object.values(response.data));
      //setFilterUsers(Object.values(response.data));
      //console.log(users);
    }
  };

  const fetchData2 = async () => {
    let response = await getProducts();
    if (response.status === 200) {
      setProducts(Object.values(response.data));
      //console.log(products);
    }
  };
  const fetchData3 = async () => {
    let response = await getCampaigns();
    if (response.status === 200) {
      setCampaignsArr(Object.values(response.data));
      setFilterCampaigns(Object.values(response.data));
    }
  };

  const handleFilter = () => {
    let filteredArr = FilterCampaigns;
    if (searchText !== "") {
      filteredArr = filteredArr.filter((campaign) =>
        campaign.Name.toLowerCase().includes(searchText.toLowerCase())
      );
      setFilterCampaigns(filteredArr);
    } /*else {
      setFilterCampaigns(FilterCampaigns);
    }*/

    if (startDate !== "" && endDate !== "") {
      filteredArr = filteredArr.filter((campaign) => {
        let createDate = campaign.CreateDate;
        if (typeof createDate === "string") {
          createDate = new Date(createDate);
        }
        return (
          moment(createDate).isBetween(startDate, endDate) ||
          moment(createDate).isSame(startDate) ||
          moment(createDate).isSame(endDate)
        );
      });
      //console.log(filteredArr);
      setFilterCampaigns(filteredArr);
    } /*else {
      setFilterCampaigns(FilterCampaigns);
    }*/
  };

  useEffect(() => {
    fetchData();
    fetchData2();
    fetchData3();
  }, []);

  return (
    <div className="campaignsPage">
      <div
        onChange={(event) => {
          handleActiveChange(event);
          setFilterCampaigns(campaignsArr);
          setSelected("Sort by");
          setStartDate("");
          setEndDate("");
          setSearchText("");
        }}
        className="wrapper5"
      >
        <input
          type="radio"
          id="Active"
          name="select"
          value="Active"
          checked={isActive === "Active"}
          onChange={handleActiveChange}
        />
        <label htmlFor="Active" className="option Active">
          <div class="dot"></div>
          <span>Active</span>
        </label>
        <input
          type="radio"
          id="UnActive"
          name="select"
          value="UnActive"
          checked={isActive === "UnActive"}
          onChange={handleActiveChange}
        />
        <label htmlFor="UnActive" className="option UnActive">
          <div class="dot"></div>
          <span>UnActive</span>
        </label>
      </div>
      <div classNameName="input-group input-group-sm mb-3 filterContainer">
        <input
          placeholder="Search campaign by Name"
          type="text"
          className="form-control filter5"
          aria-label="Small"
          aria-describedby="inputGroup-sizing-sm"
          //onChange={handleFilter}
          value={searchText}
          onChange={(e) => setSearchText(e.target.value)}
        />
      </div>
      <div className="DateRange2">
        <DatePicker
          dateFormat="yyyy-MM-dd"
          selected={startDate}
          onChange={(date) => setStartDate(date)}
          //onCalendarClose={handleFilterByDateRange}
          placeholderText="Start Date"
          className="datePosition2"
        />
        <DatePicker
          dateFormat="yyyy-MM-dd"
          selected={endDate}
          onChange={(date) => setEndDate(date)}
          //onCalendarClose={handleFilterByDateRange}
          placeholderText="End Date"
          className="datePosition2"
        />
      </div>
      <div class="setDropDownPosition2">
        <DropdownButton
          id="dropdown-basic-button"
          title={selected}
          onSelect={(eventKey) => setSelected(eventKey)}
        >
          <Dropdown.Item eventKey="Sort By">Sort By</Dropdown.Item>
          {/* <Dropdown.Item eventKey="Tweets + Retweets">
            Tweets + Retweets
          </Dropdown.Item> */}
          <Dropdown.Item
            eventKey="Products Donated"
            onClick={() => {
              let sortArray = FilterCampaigns.sort((a, b) => {
                const aValue = products.filter(
                  (obj) => obj.CampaignID === a.Id
                ).length;
                const bValue = products.filter(
                  (obj) => obj.CampaignID === b.Id
                ).length;
                return bValue - aValue;
              });
              setFilterCampaigns(sortArray);
            }}
          >
            Connected Products
          </Dropdown.Item>
          <Dropdown.Item
            eventKey="Products Donated"
            onClick={() => {
              let sortArray = FilterCampaigns.sort((a, b) => {
                const aValue = products.filter(
                  (obj) => obj.CampaignID === a.Id && obj.ActivistID === ""
                ).length;
                const bValue = products.filter(
                  (obj) => obj.CampaignID === b.Id && obj.ActivistID === ""
                ).length;
                return bValue - aValue;
              });
              setFilterCampaigns(sortArray);
            }}
          >
            Available Products
          </Dropdown.Item>
          <Dropdown.Item
            eventKey="Donation"
            onClick={() => {
              let sortArray = FilterCampaigns.sort((a, b) => {
                const aValue = products
                  .filter(
                    (obj) => obj.CampaignID === a.Id && obj.ActivistID !== ""
                  )
                  .reduce((accumulator, object) => {
                    return accumulator + object.Price;
                  }, 0);
                const bValue = products
                  .filter(
                    (obj) => obj.CampaignID === b.Id && obj.ActivistID !== ""
                  )
                  .reduce((accumulator, object) => {
                    return accumulator + object.Price;
                  }, 0);
                return bValue - aValue;
              });
              setFilterCampaigns(sortArray);
            }}
          >
            Donation
          </Dropdown.Item>
        </DropdownButton>
      </div>
      <button
        className="makeReport2"
        onClick={() => {
          handleFilter();
        }}
      >
        Make Report
      </button>
      <button
        className="resetReport2"
        onClick={() => {
          setFilterCampaigns(campaignsArr);
          setSelected("Sort by");
          setStartDate("");
          setEndDate("");
          setSearchText("");
        }}
      >
        Reset
      </button>
      <section className="section2">
        <div className="tbl-header2">
          <table cellPadding="0" cellSpacing="0" border="0">
            <thead>
              <tr>
                <th className="managerth2">ID</th>
                {/* <th className="managerth2">Organization</th> */}
                <th className="managerth2">Name</th>
                <th className="managerth2">Description</th>
                <th className="managerth2">Hashtag</th>
                <th className="managerth2">Url</th>
                <th className="managerth2">Create Date</th>
                <th className="managerth2">Connected Products</th>
                <th className="managerth2">Available Products</th>
                <th className="managerth2">Donation</th>
                {/* <th className="managerth2">Tweets</th> */}
                {/* <th className="managerth2">Users Pormoted</th> */}
                <th className="managerth2">Companies Pormoted</th>
              </tr>
            </thead>
          </table>
        </div>
        <div className="tbl-content2">
          <table cellPadding="0" cellSpacing="0" border="0">
            <tbody>
              {FilterCampaigns.map((campaign) => {
                if (campaign.Active === boolActive) {
                  //get all users except manager
                  let { Id, Name, Description, Hashtag, Url, CreateDate } =
                    campaign;
                  return (
                    <tr>
                      <td className="managertd2">{Id}</td>
                      {/* <td className="managertd2">{Hashtag}</td> */}
                      <td className="managertd2">{Name}</td>
                      <td className="managertd2">{Description}</td>
                      <td className="managertd2">{Hashtag}</td>
                      <td className="managertd2">{Url}</td>
                      <td className="managertd2">
                        <p className="spanOnlyDate2">{CreateDate}</p>
                      </td>
                      <td className="managertd2">
                        {products.filter((obj) => obj.CampaignID === Id).length}
                      </td>
                      <td className="managertd2">
                        {
                          products.filter(
                            (obj) =>
                              obj.CampaignID === Id && obj.ActivistID === ""
                          ).length
                        }
                      </td>
                      <td className="managertd2">
                        {products
                          .filter(
                            (obj) =>
                              obj.CampaignID === Id && obj.ActivistID !== ""
                          )
                          .reduce((accumulator, object) => {
                            return accumulator + object.Price;
                          }, 0)}{" "}
                        $
                      </td>
                      {/* <td className="managertd2">20</td>
                      <td className="managertd2">20</td> */}
                      <td className="managertd2">
                        {
                          products
                            .filter((obj) => obj.CampaignID === Id)
                            .filter(
                              (obj, index, self) =>
                                index ===
                                self.findIndex(
                                  (t) => t.CompanyID === obj.CompanyID
                                )
                            ).length
                        }
                      </td>
                    </tr>
                  );
                }
              })}
            </tbody>
          </table>
        </div>
      </section>
      {/* {campaignsArr.map((campaign) => {
        let { OrganizationID, Id, Name, Description, Hashtag, Url } = campaign;

        return (
          <ManagerCampaign
            OrganizationID={OrganizationID}
            Id={Id}
            Name={Name}
            Description={Description}
            Url={Url}
            Hashtag={Hashtag}
          ></ManagerCampaign>
        );
      })} */}
    </div>
  );
};
