import React, { useState, useEffect } from "react";
import "./managerUsers.css";
import { useAuth0 } from "@auth0/auth0-react";
//import { getUserById } from "../../../services/user.service";
import { getUsers } from "../../../services/user.service";
import { getProducts } from "../../../services/product.service";
import { getCampaigns } from "../../../services/campaign.service";
// import {
//   getTweetsByUser,
//   getRetweetsOfUser,
// } from "../../../services/twitter.service";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import moment from "moment";
import { Dropdown, DropdownButton } from "react-bootstrap";

export const ManagerUsersPage = (props) => {
  const { user } = useAuth0();
  const [users, setUsers] = useState([]);
  const [FilterUsers, setFilterUsers] = useState([]);
  const [roleText, setRoleText] = useState("Non-Profit Organization");
  const [products, setProducts] = useState([]);
  const [campaignsArr, setCampaignsArr] = useState([]);
  //const [tweetsAndRetweets, setTweetsAndRetweets] = useState([]);
  //const [promotedCampaigns, setPromotedCampaigns] = useState([]);
  const [startDate, setStartDate] = useState("");
  const [endDate, setEndDate] = useState("");
  const [selected, setSelected] = useState("Sort By");
  const [searchText, setSearchText] = useState("");

  const handleRoleChange = (event) => {
    setRoleText(event.target.value);
  };

  const fetchData = async () => {
    let response = await getUsers();
    if (response.status === 200) {
      setUsers(Object.values(response.data));
      setFilterUsers(Object.values(response.data));
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
    }
  };

  // const getTweets = async (twitterHandle, hashtag) => {
  //   let sum = 0;
  //   let response = await getTweetsByUser(twitterHandle, hashtag);
  //   if (response.status === 200) {
  //     //console.log(response.data.meta.result_count);
  //     sum = sum + response.data.meta.result_count;
  //   }
  //   return sum;
  // };

  // const getRetweets = async (twitterHandle, hashtag) => {
  //   let sum = 0;
  //   let response = await getRetweetsOfUser(twitterHandle, hashtag);
  //   if (response.status === 200) {
  //     //console.log(response.data.meta.result_count);
  //     sum = sum + response.data.meta.result_count;
  //   }
  //   return sum;
  // };

  const handleFilter = () => {
    let filteredArr = FilterUsers;
    if (searchText !== "") {
      filteredArr = filteredArr.filter((user) =>
        user.Name.toLowerCase().includes(searchText.toLowerCase())
      );
      setFilterUsers(filteredArr);
    } /*else {
      setFilterUsers(FilterUsers);
    }*/

    if (startDate !== "" && endDate !== "") {
      filteredArr = filteredArr.filter((user) => {
        let createDate = user.CreateDate;
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
      setFilterUsers(filteredArr);
    } /*else {
      setFilterUsers(FilterUsers);
    }*/
  };

  // const handleFilterByDateRange = () => {
  //   if (startDate !== "" && endDate !== "") {
  //     let filteredArr = FilterUsers.filter((user) => {
  //       let createDate = user.CreateDate;
  //       if (typeof createDate === "string") {
  //         createDate = new Date(createDate);
  //       }
  //       return (
  //         moment(createDate).isBetween(startDate, endDate) ||
  //         moment(createDate).isSame(startDate) ||
  //         moment(createDate).isSame(endDate)
  //       );
  //     });
  //     //console.log(filteredArr);
  //     setFilterUsers(filteredArr);
  //   } else {
  //     setFilterUsers(FilterUsers);
  //   }
  // };

  // const getPromotedNum = async (userId, TwitterHandle) => {
  //   let sum1 = 0;
  //   let sum2 = 0;
  //   for (let i = 0; i < campaignsArr.length; i++) {
  //     console.log(campaignsArr[i].Hashtag);
  //     sum1 = await getTweets(TwitterHandle, campaignsArr[i].Hashtag);
  //     if (sum1 > 0) {
  //       sum2 = sum2 + 1;
  //     }
  //   }
  //   let newData = {
  //     id: userId,
  //     campaignsSum: sum2,
  //   };
  //   setPromotedCampaigns([...promotedCampaigns, newData]);
  //   console.log(newData);
  //   console.log(promotedCampaigns);
  //   let object = promotedCampaigns.find((obj) => obj.id === userId);
  //   console.log(object.campaignsSum);
  // };

  useEffect(() => {
    fetchData();
    fetchData2();
    fetchData3();
    //handleFilterByDateRange();
  }, []);

  return (
    <div className="productsPage">
      <section>
        {/* <h1 className="managertbl-h1">Users List</h1> */}
        <div
          onChange={(event) => {
            handleRoleChange(event);
            setFilterUsers(users);
            setSelected("Sort by");
            setStartDate("");
            setEndDate("");
            setSearchText("");
          }}
          className="wrapper4"
        >
          <input
            type="radio"
            id="Non-Profit-Organization"
            name="select"
            value="Non-Profit Organization"
            checked={roleText === "Non-Profit Organization"}
            onChange={handleRoleChange}
          />
          <label
            htmlFor="Non-Profit-Organization"
            className="option Non-Profit-Organization"
          >
            <div class="dot"></div>
            <span>Organization</span>
          </label>
          <input
            type="radio"
            id="Company"
            name="select"
            value="Company"
            checked={roleText === "Company"}
            onChange={handleRoleChange}
          />
          <label htmlFor="Company" className="option Company">
            <div class="dot"></div>
            <span>Company</span>
          </label>
          <input
            type="radio"
            id="Social-Activist"
            name="select"
            value="Social Activist"
            checked={roleText === "Social Activist"}
            onChange={handleRoleChange}
          />
          <label htmlFor="Social-Activist" className="option Social-Activist">
            <div class="dot"></div>
            <span>Social Activist</span>
          </label>
        </div>
        <div classNameName="input-group input-group-sm mb-3 filterContainer">
          <input
            placeholder="Search user by Name"
            type="text"
            className="form-control filter4"
            aria-label="Small"
            aria-describedby="inputGroup-sizing-sm"
            //onChange={handleFilter}
            value={searchText}
            onChange={(e) => setSearchText(e.target.value)}
          />
        </div>
        <div className="DateRange">
          <DatePicker
            dateFormat="yyyy-MM-dd"
            selected={startDate}
            onChange={(date) => setStartDate(date)}
            //onCalendarClose={handleFilterByDateRange}
            placeholderText="Start Date"
            className="datePosition"
          />
          <DatePicker
            dateFormat="yyyy-MM-dd"
            selected={endDate}
            onChange={(date) => setEndDate(date)}
            //onCalendarClose={handleFilterByDateRange}
            placeholderText="End Date"
            className="datePosition"
          />
        </div>
        <div class="setDropDownPosition">
          <DropdownButton
            id="dropdown-basic-button"
            title={selected}
            onSelect={(eventKey) => setSelected(eventKey)}
          >
            <Dropdown.Item eventKey="Sort By">Sort By</Dropdown.Item>
            {roleText === "Social Activist" ? (
              <>
                <Dropdown.Item
                  eventKey="Tweets + Retweets"
                  onClick={() => {
                    let sortArray = FilterUsers.sort((a, b) => {
                      const aValue =
                        a.Status +
                        products
                          .filter((obj) => obj.ActivistID === a.UserID)
                          .reduce((accumulator, object) => {
                            return accumulator + object.Price;
                          }, 0);
                      const bValue =
                        b.Status +
                        products
                          .filter((obj) => obj.ActivistID === b.UserID)
                          .reduce((accumulator, object) => {
                            return accumulator + object.Price;
                          }, 0);
                      return bValue - aValue;
                    });
                    setFilterUsers(sortArray);
                  }}
                >
                  Tweets + Retweets
                </Dropdown.Item>
                <Dropdown.Item
                  eventKey="Products Bought"
                  onClick={() => {
                    let sortArray = FilterUsers.sort((a, b) => {
                      const aValue = products.filter(
                        (obj) => obj.ActivistID === a.UserID
                      ).length;
                      const bValue = products.filter(
                        (obj) => obj.ActivistID === b.UserID
                      ).length;
                      return bValue - aValue;
                    });
                    setFilterUsers(sortArray);
                  }}
                >
                  Products Bought
                </Dropdown.Item>
                <Dropdown.Item
                  eventKey="Products Donated"
                  onClick={() => {
                    let sortArray = FilterUsers.sort((a, b) => {
                      const aValue = products.filter(
                        (obj) =>
                          obj.ActivistID === a.UserID &&
                          obj.DonatedByActivist === true
                      ).length;
                      const bValue = products.filter(
                        (obj) =>
                          obj.ActivistID === b.UserID &&
                          obj.DonatedByActivist === true
                      ).length;
                      return bValue - aValue;
                    });
                    setFilterUsers(sortArray);
                  }}
                >
                  Products Donated
                </Dropdown.Item>
              </>
            ) : roleText === "Company" ? (
              <>
                <Dropdown.Item
                  eventKey="Products Donated"
                  onClick={() => {
                    let sortArray = FilterUsers.sort((a, b) => {
                      const aValue = products.filter(
                        (obj) => obj.CompanyID === a.UserID
                      ).length;
                      const bValue = products.filter(
                        (obj) => obj.CompanyID === b.UserID
                      ).length;
                      return bValue - aValue;
                    });
                    setFilterUsers(sortArray);
                  }}
                >
                  Products Donated
                </Dropdown.Item>
                <Dropdown.Item
                  eventKey="Products Bought"
                  onClick={() => {
                    let sortArray = FilterUsers.sort((a, b) => {
                      const aValue = products.filter(
                        (obj) =>
                          obj.CompanyID === a.UserID && obj.ActivistID !== ""
                      ).length;
                      const bValue = products.filter(
                        (obj) =>
                          obj.CompanyID === b.UserID && obj.ActivistID !== ""
                      ).length;
                      return bValue - aValue;
                    });
                    setFilterUsers(sortArray);
                  }}
                >
                  Products Bought
                </Dropdown.Item>
                <Dropdown.Item
                  eventKey="Donation"
                  onClick={() => {
                    let sortArray = FilterUsers.sort((a, b) => {
                      const aValue = products
                        .filter(
                          (obj) =>
                            obj.CompanyID === a.UserID &&
                            obj.DonatedByActivist === false
                        )
                        .reduce((accumulator, object) => {
                          return accumulator + object.Price;
                        }, 0);
                      const bValue = products
                        .filter(
                          (obj) =>
                            obj.CompanyID === b.UserID &&
                            obj.DonatedByActivist === false
                        )
                        .reduce((accumulator, object) => {
                          return accumulator + object.Price;
                        }, 0);
                      return bValue - aValue;
                    });
                    setFilterUsers(sortArray);
                  }}
                >
                  Donation
                </Dropdown.Item>
                <Dropdown.Item
                  eventKey="Promoted Campaigns
"
                  onClick={() => {
                    let sortArray = FilterUsers.sort((a, b) => {
                      const aValue = products
                        .filter((obj) => obj.CompanyID === a.UserID)
                        .filter(
                          (obj, index, self) =>
                            index ===
                            self.findIndex(
                              (t) => t.CampaignID === obj.CampaignID
                            )
                        ).length;
                      const bValue = products
                        .filter((obj) => obj.CompanyID === b.UserID)
                        .filter(
                          (obj, index, self) =>
                            index ===
                            self.findIndex(
                              (t) => t.CampaignID === obj.CampaignID
                            )
                        ).length;
                      return bValue - aValue;
                    });
                    setFilterUsers(sortArray);
                  }}
                >
                  Promoted Campaigns
                </Dropdown.Item>
              </>
            ) : roleText === "Non-Profit Organization" ? (
              <>
                <Dropdown.Item
                  eventKey="Products Donated"
                  onClick={() => {
                    let sortArray = FilterUsers.sort((a, b) => {
                      const aValue = products.filter(
                        (obj) => obj.OrganizationID === a.UserID
                      ).length;
                      const bValue = products.filter(
                        (obj) => obj.OrganizationID === b.UserID
                      ).length;
                      return bValue - aValue;
                    });
                    setFilterUsers(sortArray);
                  }}
                >
                  Products Donated
                </Dropdown.Item>
                <Dropdown.Item
                  eventKey="Donation"
                  onClick={() => {
                    let sortArray = FilterUsers.sort((a, b) => {
                      const aValue = products
                        .filter(
                          (obj) =>
                            obj.OrganizationID === a.UserID &&
                            obj.ActivistID !== ""
                        )
                        .reduce((accumulator, object) => {
                          return accumulator + object.Price;
                        }, 0);
                      const bValue = products
                        .filter(
                          (obj) =>
                            obj.OrganizationID === b.UserID &&
                            obj.ActivistID !== ""
                        )
                        .reduce((accumulator, object) => {
                          return accumulator + object.Price;
                        }, 0);
                      return bValue - aValue;
                    });
                    setFilterUsers(sortArray);
                  }}
                >
                  Donation
                </Dropdown.Item>
              </>
            ) : (
              <></>
            )}
          </DropdownButton>
        </div>
        <button
          className="makeReport"
          onClick={() => {
            handleFilter();
          }}
        >
          Make Report
        </button>
        <button
          className="resetReport"
          onClick={() => {
            setFilterUsers(users);
            setSelected("Sort by");
            setStartDate("");
            setEndDate("");
            setSearchText("");
          }}
        >
          Reset
        </button>
        <div className="tbl-header">
          <table cellPadding="0" cellSpacing="0" border="0">
            <thead>
              <tr>
                <th className="managerth">User ID</th>
                {/* <th className="managerth">Role</th> */}
                <th className="managerth">Name</th>
                <th className="managerth">Address</th>
                <th className="managerth">Phone</th>
                {roleText === "Non-Profit Organization" ? (
                  <th className="managerth">Url</th>
                ) : (
                  <></>
                )}
                {roleText === "Social Activist" ? (
                  <th className="managerth">Status</th>
                ) : (
                  <></>
                )}
                {roleText === "Social Activist" || roleText === "Company" ? (
                  <th className="managerth">Twitter Handle</th>
                ) : (
                  <></>
                )}
                <th className="managerth">Create Date</th>
                {
                  /*roleText === "Non-Profit Organization" ||*/
                  roleText === "Social Activist" ? (
                    <th className="managerth">tweets + retweets</th>
                  ) : (
                    <></>
                  )
                }
                {roleText === "Social Activist" || roleText === "Company" ? (
                  <th className="managerth">Products Bought</th>
                ) : (
                  <></>
                )}
                <th className="managerth">Products Donated</th>
                {roleText === "Non-Profit Organization" ||
                roleText === "Company" ? (
                  <th className="managerth">Donation</th>
                ) : (
                  <></>
                )}
                {
                  /*roleText === "Social Activist" ||*/ roleText ===
                  "Company" ? (
                    <th className="managerth">Promoted Campaigns</th>
                  ) : (
                    <></>
                  )
                }
              </tr>
            </thead>
          </table>
        </div>
        <div className="tbl-content">
          <table cellPadding="0" cellSpacing="0" border="0">
            <tbody>
              {FilterUsers.map((user) => {
                if (user.Role === roleText) {
                  //get all users except manager
                  let {
                    UserID,
                    Role,
                    Name,
                    Address,
                    Phone,
                    Url,
                    Status,
                    TwitterHandle,
                    CreateDate,
                  } = user;
                  return (
                    <tr>
                      <td className="managertd">{UserID}</td>
                      {/* <td className="managertd">{Role}</td> */}
                      <td className="managertd">{Name}</td>
                      <td className="managertd">{Address}</td>
                      <td className="managertd">{Phone}</td>
                      {roleText === "Non-Profit Organization" ? (
                        <td className="managertd">{Url}</td>
                      ) : (
                        <></>
                      )}
                      {roleText === "Social Activist" ? (
                        <td className="managertd">{Status}</td>
                      ) : (
                        <></>
                      )}
                      {roleText === "Social Activist" ||
                      roleText === "Company" ? (
                        <td className="managertd">{TwitterHandle}</td>
                      ) : (
                        <></>
                      )}
                      <td className="managertd">
                        <p className="spanOnlyDate2">{CreateDate}</p>
                      </td>
                      {roleText === "Non-Profit Organization" ? (
                        <></> // <td className="managertd">tweets</td>
                      ) : roleText === "Social Activist" ? (
                        <td
                          className="managertd"
                          // onClick={async () => {
                          //   let sum1 = 0;
                          //   let sum2 = 0;
                          //   for (let i = 0; i < campaignsArr.length; i++) {
                          //     console.log(campaignsArr[i].Hashtag);
                          //     sum1 =
                          //       sum1 +
                          //       (await getTweets(
                          //         TwitterHandle,
                          //         campaignsArr[i].Hashtag
                          //       ));
                          //     sum2 =
                          //       sum2 +
                          //       (await getRetweets(
                          //         TwitterHandle,
                          //         campaignsArr[i].Hashtag
                          //       ));
                          //   }
                          //   let newData = {
                          //     id: UserID,
                          //     sumOfTweetsAndRetweets: sum1 + sum2,
                          //   };
                          //   setTweetsAndRetweets((prev) => [...prev, newData]);
                          // }}
                        >
                          {Status +
                            products
                              .filter((obj) => obj.ActivistID === UserID)
                              .reduce((accumulator, object) => {
                                return accumulator + object.Price;
                              }, 0)}
                          {/*tweetsAndRetweets.filter(
                              (obj) => obj.id === UserID
                            )[0].sumOfTweetsAndRetweets*/}
                        </td>
                      ) : (
                        <></>
                      )}
                      {roleText === "Social Activist" ? (
                        <td className="managertd">
                          {
                            products.filter((obj) => obj.ActivistID === UserID)
                              .length
                          }
                        </td>
                      ) : roleText === "Company" ? (
                        <td className="managertd">
                          {
                            products.filter(
                              (obj) =>
                                obj.CompanyID === UserID &&
                                obj.ActivistID !== ""
                            ).length
                          }
                        </td>
                      ) : (
                        <></>
                      )}
                      {roleText === "Social Activist" ? (
                        <td className="managertd">
                          {
                            products.filter(
                              (obj) =>
                                obj.ActivistID === UserID &&
                                obj.DonatedByActivist === true
                            ).length
                          }
                        </td>
                      ) : roleText === "Company" ? (
                        <td className="managertd">
                          {
                            products.filter((obj) => obj.CompanyID === UserID)
                              .length
                          }
                        </td>
                      ) : (
                        <td className="managertd">
                          {
                            products.filter(
                              (obj) => obj.OrganizationID === UserID
                            ).length
                          }
                        </td>
                      )}
                      {roleText === "Non-Profit Organization" ? (
                        <td className="managertd">
                          {products
                            .filter(
                              (obj) =>
                                obj.OrganizationID === UserID &&
                                obj.ActivistID !== ""
                            )
                            .reduce((accumulator, object) => {
                              return accumulator + object.Price;
                            }, 0)}{" "}
                          $
                        </td>
                      ) : roleText === "Company" ? (
                        <td className="managertd">
                          {products
                            .filter(
                              (obj) =>
                                obj.CompanyID === UserID &&
                                obj.DonatedByActivist === false
                            )
                            .reduce((accumulator, object) => {
                              return accumulator + object.Price;
                            }, 0)}{" "}
                          $
                        </td>
                      ) : (
                        <></>
                      )}
                      {
                        /*roleText === "Social Activist" ? (
                        <td
                          className="managertd"
                          onClick={async () => {
                            getPromotedNum(UserID, TwitterHandle);
                          }}
                        >
                          {promotedCampaigns.filter((obj) => obj.id === UserID)
                            .campaignsSum === undefined ? (
                            <>0</>
                          ) : (
                            <></>
                          )}
                        </td>
                      ) :*/ roleText === "Company" ? (
                          <td className="managertd">
                            {
                              products
                                .filter((obj) => obj.CompanyID === UserID)
                                .filter(
                                  (obj, index, self) =>
                                    index ===
                                    self.findIndex(
                                      (t) => t.CampaignID === obj.CampaignID
                                    )
                                ).length
                            }
                          </td>
                        ) : (
                          <></>
                        )
                      }
                    </tr>
                  );
                }
              })}
            </tbody>
          </table>
        </div>
      </section>
    </div>
  );
};
