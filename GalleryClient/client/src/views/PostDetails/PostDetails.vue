<template>
  <div class="details">
    <p v-if="state.message" class="message">{{state.message}}</p>
    <div class="post-header">
      <router-link :to="getProfileLink()" class="profile-image">
        <img v-if="state.post.profilePicture" :src="state.post.profilePicture" />
        <img v-else src="@/assets/icons/profile.png" />
        <p>Created by: {{state.post.userName}}</p>
      </router-link>
      <div>
        <img v-if="state.isLiked" class="heart-icon" src="@/assets/icons/heart-solid.svg" @click="unlike" alt="liked heart" />
        <img v-else class="heart-icon" src="@/assets/icons/heart-regular.svg" @click="like" alt="like heart" />
        <p>Likes: {{ state.likes }}</p>
        
        <router-link :to="getEditRoute()" v-if="state.isAuthor">
          <img class="edit-icon" src="@/assets/icons/edit.svg" @click="edit" alt="edit icon" />
        </router-link>
        <router-link to="/delete" v-if="state.isAuthor">
          <img class="delete-icon" src="@/assets/icons/delete.svg" @click="deletePost" alt="delete icon" />
        </router-link>
      </div>
    </div>

    <img v-if="state.loading" class="loader" src="@/assets/loading.gif" />
    <img :src="state.post.picture" :alt="state.post.description" /><br />

    <div class="post-footer">
      <div>
        <img
          class="calendar-icon"
          src="@/assets/icons/calendar.svg"
          alt="calendar"
        /><br>
        <p>{{ state.createdOn }}</p>
      </div>
      <div>
        <img
          class="info-icon"
          src="@/assets/icons/info-solid.svg"
          alt="calendar"
        />
        <p>{{ state.post.description }}</p>
      </div>
      <div v-if="state.post.location">
        <img
          class="location-icon"
          src="@/assets/icons/map-marker-solid.svg"
          alt="location"
        />
        <p>{{ state.post.location || "none" }}</p>
      </div>      
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, reactive, watchEffect } from "vue";
import moment from "moment";
import router from "../../router";
import { postService, profileService } from "../../services";

export default defineComponent({
  setup() {
    const state = reactive({
      post: [],
      likes: 0,
      isLiked: false,
      createdOn: null,      
      message: '',
      isAuthor: false
    });

    const getProfileLink = () => "/profile/" + state.post.authorId;
    const getEditRoute = () => "/edit/" + state.post.id;

    watchEffect(async () => {
      const id = window.location.href.split("/")[3];
      const post = await postService.getById(id);
      const profile = await profileService.get();

      if(profile.id === post.authorId) {
        state.isAuthor = true;
      }

      state.likes = post.likes;
      state.isLiked = post.isLiked;
      state.post = post;      
      state.createdOn = moment(post.createdOn, "YYYYMMDD").fromNow();
    });

    const like = async() => {
      let status = await postService.like(localStorage.getItem('token'), state.post.id);
      
      if(status == 200) {
        state.likes = state.post.likes + 1;
        state.isLiked = true;
      } else if(status == 400) {
        state.message = "You had already liked this post.";
      } else {
        router.push("login");
      }
    }
    
    const unlike = async() => {
      let status = await postService.unlike(localStorage.getItem('token'), state.post.id);      
      if(status == 200) {
        state.likes = state.likes - 1;          
        state.isLiked = false;          
      } else if(status == 400) {
        state.message = "You had not liked this post.";
      } else {
        router.push("login");
      }
    }

    return {
      state,
      getProfileLink,
      getEditRoute,
      like,
      unlike
    };
  }
});
</script>

<styles src="./index.scss"></styles>