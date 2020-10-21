<template>
  <div class="profile-details">
    <p class="error">{{state.error}}</p>
    <img v-if="state.loading" class="loader" src="@/assets/loading.gif" />
    <div v-else class="profile">
      <img
        v-if="state.profile.picture"
        :src="state.picture || state.profile.picture"
        class="profile-icon"
      />
      <img v-else src="@/assets/icons/profile.png" class="profile-icon" />
      <input id="upload" type="file" @change="uploadImage" />
      <router-link to="#" id="upload-link" @click="clickImage">Change profile image</router-link>

      <div class="info">
        <b>Email:</b>
        <p>{{ state.profile.email }}</p>
        <b>Username:</b>
        <p>{{ state.profile.userName }}</p>
      </div>

      <div class="wrapper">
        <div class="tabs">
          <div class="tab">
            <input
              type="radio"
              name="css-tabs"
              id="tab-1"
              checked
              class="tab-switch"
            />
            <label for="tab-1" class="tab-label">Photos</label>
            <div class="tab-content">
              <h1 v-if="state.userPosts.length === 0" class="empty-message">No posts</h1>
              <div v-else>
                <div class="posts">
                  <router-link :to="post.id" v-for="post in state.userPosts" :key="post.id"> 
                    <img :src="post.picture" class="image" :alt="post.description" />
                  </router-link>
                </div>

                <div v-if="state.userPosts.length >= 3">
                  <button v-if="!state.allPosts" @click="loadMorePosts" class="load-more">Load more</button>
                  <button v-else @click="loadLessPosts" class="load-less">Load less</button>
                </div>              
            </div>
          </div>
          </div>
          <div class="tab">
            <input type="radio" name="css-tabs" id="tab-2" class="tab-switch" />
            <label for="tab-2" class="tab-label">Liked</label>
            <div class="tab-content">
              <div class="posts">
                <h1 v-if="state.userLikedPosts.length === 0" class="empty-message">No liked posts</h1>
                <router-link v-else :to="post.id" v-for="post in state.userLikedPosts" :key="post.id">
                  <div class="image">
                    <img :src="post.picture" :alt="post.description" />          
                  </div>
                  </router-link>
              </div>

              <div v-if="state.userLikedPosts.length >= 3">
                <button v-if="!state.allLikedPosts" @click="loadMoreLikedPosts" class="load-more">Load more</button>
                <button v-else @click="loadLessLikedPosts" class="load-less">Load less</button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, reactive, watchEffect } from "vue";
import { profileService } from "../../services";
import store from '../../store/index.js';

export default defineComponent({
  setup() {
    const state = reactive({
      profile: {},
      loading: true,
      userPosts: [],
      userLikedPosts: [],
      allPosts: false,
      allLikedPosts: false,
      picture: '',
      error: '',
      maxSize: 15728640,
    });

    watchEffect(async () => {      
      const id = window.location.href.split("/")[4];
      
      if(!id) {
        const profile = await profileService.get();
        const userPosts = await profileService.getUserPosts(state.allPosts);
        const userLikedPosts = await profileService.getUserLikedPosts(state.allLikedPosts);

        state.userPosts = userPosts.posts;
        state.userLikedPosts = userLikedPosts.posts;
        state.profile = profile;
        state.loading = false;
      } else {
        const profile = await profileService.getById(id);
        const userPosts = await profileService.getUserPostsById(state.allPosts, id);
        const userLikedPosts = await profileService.getUserLikedPosts(state.allLikedPosts, id);

        state.userPosts = userPosts.posts;
        state.userLikedPosts = userLikedPosts.posts;
        state.profile = profile;
        state.loading = false;
      }
    });

    const clickImage = () => {
      document.querySelector('#upload').click();
    }

    const uploadImage = async(e) => {
      let file = e.target.files[0];

      if (!e.target.files.length) return;

      if (e.target.files.length === 1) {
        if (file.size > state.maxSize) {
          state.error = "Too large picture!";
          return;
        } else {
          state.picture = file;
        }
      } else {
        state.error = "Only one photo is allowed!";
        return;
      }

      const data = await profileService.uploadProfileImage(state.picture);
      store.dispatch('setProfilePicture', data);
      state.picture = data;
      window.location.href = "/profile";
    }

    const loadMorePosts = async() => {
      state.allPosts = true;
      const userPosts = await profileService.getUserPosts(state.allPosts);      
      state.userPosts = userPosts.posts;
    }

    const loadLessPosts = async() => {
      state.allPosts = false;
      const userPosts = await profileService.getUserPosts(state.allPosts);      
      state.userPosts = userPosts.posts;
    }
    
    const loadMoreLikedPosts = async() => {
      state.allLikedPosts = true;
      const userLikedPosts = await profileService.getUserLikedPosts(state.allLikedPosts);
      state.userLikedPosts = userLikedPosts.posts;
    }

    const loadLessLikedPosts = async() => {
      state.allLikedPosts = false;
      const userLikedPosts = await profileService.getUserLikedPosts(state.allLikedPosts);      
      state.userLikedPosts = userLikedPosts.posts;
    }

    return {
      state,
      clickImage,
      uploadImage,
      loadMorePosts,
      loadLessPosts,
      loadMoreLikedPosts,
      loadLessLikedPosts
    };
  }
});
</script>

<styles src="./index.scss"></styles>