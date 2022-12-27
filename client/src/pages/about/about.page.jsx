import React from "react";
import "./style.css";

export const AboutPage = (props) => {
  return (
    <div className="about">
      <div>
        <p className="text">
          NorthWind watches are crafted with scrupulous attention to detail.
          Explore the NorthWind collection of prestigious, high-precision
          timepieces. NorthWind offers a wide assortment of Classic and
          Professional watch models to suit any wrist. Discover the broad
          selection of NorthWind watches to find a perfect combination of style
          and functionality.
        </p>
      </div>
      <div class="contact-info">
        <h4>General Inquiries</h4>
        <p className="ques">Have a question… or just want to say hello?</p>
        <p className="ques">
          <a
            class="gold-text-button"
            href="mailto:ofermordehai0@gmail.com"
            target="_blank"
            rel="noopener"
          >
            ofermordehai0@gmail.com
          </a>
          <a class="gold-text-button" target="_blank" rel="noopener">
            0528016257
          </a>
        </p>
        <h4>Our Location</h4>
        <p className="ques">Moshe Sharet 6</p>
        <p className="ques">Israel, Akko</p>
        <p className="ques">
          <a
            class="gold-text-button"
            href="https://goo.gl/maps/4vMTgzd5zE98AqrTA"
            target="_blank"
            rel="noopener"
          >
            GET DIRECTIONS
          </a>
        </p>
      </div>
    </div>
  );
};
