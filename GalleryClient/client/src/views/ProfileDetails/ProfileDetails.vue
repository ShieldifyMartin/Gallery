<template>
  <div class="profile-details">
    <img v-if="state.loading" class="loader" src="@/assets/loading.gif" />
    <div v-else class="profile">
      <img
        v-if="state.profile.picture"
        :src="state.profile.picture"
        class="profile-icon"
      />
      <img v-else src="@/assets/icons/profile.png" class="profile-icon" />
      <router-link to="/#">Change profile picture</router-link>

      <div class="info">
        <p>Email: {{ state.profile.email }}</p>
        <p>Username: {{ state.profile.userName }}</p>
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
                <button v-if="!state.all" @click="loadMore" class="load-more">Load more</button>
                <button v-else @click="loadLess" class="load-less">Load less</button>
              </div>
          </div>
          </div>
          <div class="tab">
            <input type="radio" name="css-tabs" id="tab-2" class="tab-switch" />
            <label for="tab-2" class="tab-label">Liked</label>
            <div class="tab-content">
              <div class="liked-posts">
                <h1 v-if="state.userLikedPosts.length === 0" class="empty-message">No liked posts</h1>
                <router-link v-else :to="post.id" v-for="post in state.userLikedPosts" :key="post.id">
                  <div class="image">
                    <img :src="post.picture" :alt="post.description" />          
                  </div>
                  </router-link>
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

export default defineComponent({
  setup() {
    const state = reactive({
      profile: {},
      loading: true,
      userPosts: [],
      userLikedPosts: [],
      all: false
    });    

    watchEffect(async () => {
      const profile = await profileService.get();
      const userPosts = await profileService.getUserPosts(state.all);
      const userLikedPosts = await profileService.getUserLikedPosts(state.all);
      
      state.userPosts = userPosts.posts;
      state.userLikedPosts = userLikedPosts.posts;
      state.profile = profile;
      state.loading = false;
    });

    const loadMore = async() => {
      state.all = true;

      const userPosts = await profileService.getUserPosts(state.all);      
      state.userPosts = userPosts.posts;             
    }

    const loadLess = async() => {
      state.all = false;
      
      const userPosts = await profileService.getUserPosts(state.all);      
      state.userPosts = userPosts.posts;             
    }

    return {
      state,
      loadMore,
      loadLess
    };
  }
});
</script>

<styles src="./index.scss"></styles>