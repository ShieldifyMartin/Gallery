<template>
  <div class="details">
    <p v-if="state.message" class="message">{{ state.message }}</p>
    <div v-if="state.deleteAlert" class="delete-alert">
      <h1>Delete Post</h1>
      <p>Are you sure you want to delete your post?</p>

      <div class="clearfix">
        <button type="button" class="cancel-btn" @click="toggleDeletePostAlert">
          Cancel
        </button>
        <button type="button" class="delete-btn" @click="deletePost">
          Delete
        </button>
      </div>
    </div>
    <div class="post-header">
      <router-link :to="getProfileLink()" class="profile-image">
        <img
          v-if="state.post.profilePicture"
          :src="state.post.profilePicture"
        />
        <img v-else src="@/assets/icons/profile.png" />
        <p>Created by: {{ state.post.userName }}</p>
      </router-link>
      <div>
        <img
          v-if="state.isLiked"
          class="heart-icon"
          src="@/assets/icons/heart-solid.svg"
          @click="unlike"
          alt="liked heart"
        />
        <img
          v-else
          class="heart-icon"
          src="@/assets/icons/heart-regular.svg"
          @click="like"
          alt="like heart"
        />
        <p>Likes: {{ state.likes }}</p>

        <div class="author-icons">
          <router-link :to="getEditRoute()" v-if="state.isAuthor">
            <img
              class="edit-icon"
              src="@/assets/icons/edit.svg"
              @click="edit()"
              alt="edit icon"
            />
          </router-link>
          <div v-if="state.isAuthor">
            <img
              class="delete-icon"
              src="@/assets/icons/delete.svg"
              @click="toggleDeletePostAlert()"
              alt="delete icon"
            />
          </div>
        </div>
      </div>
    </div>

    <img v-if="state.loading" class="loader" src="@/assets/loading.gif" />
    <img :src="state.post.picture" class="post-image" :alt="state.post.description" /><br />

    <div class="post-footer">
      <div>
        <img
          class="calendar-icon"
          src="@/assets/icons/calendar.svg"
          alt="calendar"
        /><br />
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
        <p>{{ state.post.location !== "null" ? state.post.location : "none" }}</p>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, reactive, watchEffect } from "vue";
import moment from "moment";
import router from "../../router";
import store from "../../store";
import { postService, profileService } from "../../services";

export default defineComponent({
  setup() {
    const state = reactive({
      post: [],
      likes: 0,
      isLiked: false,
      deleteAlert: false,
      createdOn: null,
      message: "",
      isAuthor: false,
    });

    const getProfileLink = () => "/profile/" + state.post.authorId;
    const getEditRoute = () => "/edit/" + state.post.id;

    watchEffect(async () => {
      const id = window.location.href.split("/")[3];
      const post = await postService.getById(id);

      if (store.state.auth.state.isAuth) {
        const profile = await profileService.get();
        if (profile.id === post.authorId) {
          state.isAuthor = true;
        }
      }

      state.likes = post.likes;
      state.isLiked = post.isLiked;
      state.post = post;
      state.createdOn = moment(post.createdOn, "YYYYMMDD").fromNow();
    });

    const like = async () => {
      let status = await postService.like(
        store.state.auth.state.token,
        state.post.id
      );

      if (status == 200) {
        state.likes = state.post.likes + 1;
        state.isLiked = true;
      } else if (status == 400) {
        state.message = "You had already liked this post.";
      } else {
        router.push("login");
      }
    };

    const unlike = async () => {
      let status = await postService.unlike(
        store.state.auth.state.token,
        state.post.id
      );
      if (status == 200) {
        state.likes = state.likes - 1;
        state.isLiked = false;
      } else if (status == 400) {
        state.message = "You had not liked this post.";
      } else {
        router.push("login");
      }
    };

    const toggleDeletePostAlert = () => {
      state.deleteAlert = !state.deleteAlert;
    };

    const deletePost = async () => {
      let status = await postService.deletePost(
        store.state.auth.state.token,
        state.post.id
      );

      if (status == 200) {
        router.push("/");
      } else if (status == 400) {
        state.message = "Something went wrong.";
        toggleDeletePostAlert();
      } else {
        router.push("login");
      }
    };

    return {
      state,
      getProfileLink,
      getEditRoute,
      like,
      unlike,
      toggleDeletePostAlert,
      deletePost,
    };
  },
});
</script>

<styles src="./index.scss"></styles>
