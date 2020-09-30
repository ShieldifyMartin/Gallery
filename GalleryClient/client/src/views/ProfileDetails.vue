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
              <div class="posts">
                <router-link :to="post.id" v-for="post in state.userPosts" :key="post.id">
                  <div class="image">
                    <img :src="post.picture" :alt="post.description" />          
                  </div>
                </router-link>
              </div>

              <button @click="loadMore">Load more</button>
            </div>
          </div>
          <div class="tab">
            <input type="radio" name="css-tabs" id="tab-2" class="tab-switch" />
            <label for="tab-2" class="tab-label">Liked</label>
            <div class="tab-content">
              hematics, useful to those who intend to travel, as I always
              believed it would be, some time or other, my fortune to do.
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, reactive, watchEffect } from "vue";
import { profileService } from "../services";

export default defineComponent({
  setup() {
    const state = reactive({
      profile: {},
      loading: true,
      userPosts: [],
      page: 0      
    });    

    watchEffect(async () => {      
      const profile = await profileService.get();
      const userPosts = await profileService.getUserPosts(state.page);
      
      state.userPosts = userPosts.posts;
      state.page = state.page + 1;
      state.profile = profile;
      state.loading = false;
    });

    const loadMore = async() => {
      const userPosts = await profileService.getUserPosts(state.page);
      state.userPosts = userPosts.posts;
      state.page = state.page + 1;
    }

    return {
      state,
      loadMore
    };
  }
});
</script>

<style lang="scss">
.profile-details {
  .loader {
    margin: 0 auto;
    width: 7em;
  }

  .profile {
    text-align: center;

    .profile-icon {
      display: block;
      width: 6em;
      margin: 0 auto;
    }

    a {
      display: block;
      text-decoration: none;
      color: #a0a0a0;
      margin-bottom: 2em;
    }

    .wrapper {
      width: 100%;
      margin: 0 auto;

      .tabs {
        display: flex;
        position: relative;
        margin: 3em 2em;
        height: 14.75rem;
      }
      .tabs::before,
      .tabs::after {
        content: "";
        display: table;
      }
      .tabs::after {
        clear: both;
      }

      .tab-switch {
        display: none;
      }
      .tab-label {
        position: relative;
        display: block;
        line-height: 2.75em;
        height: 3em;
        padding: 0 1.618em;
        background: #fff;
        cursor: pointer;
        top: 0;
        transition: all 0.25s;
      }
      .tab-content {
        height: 12rem;
        position: absolute;
        z-index: 1;
        top: 2.75em;
        left: 0;
        padding: 1.618rem;
        background: #fff;
        color: #2c3e50;
        opacity: 0;
        transition: all 0.35s;

        .posts {
          display: grid;
          position: relative;
          grid-template-columns: repeat(auto-fill, minmax(17rem, 1fr));
          grid-gap: 4em;
          margin: 2em auto;          
        }
        .posts a {
          text-decoration: none;
          color: #000;
        }
        .posts img {
          width: 100%;
          height: 15em;
          object-fit: cover;
          border-radius: 0.75rem;        
          background-size:100%;
        }
      }
      .tab-switch:checked + .tab-label {
        background: #1abc9c;
        color: #ffff;        
        transition: all 0.35s;
        z-index: 1;        
      }
      .tab-switch:checked + label + .tab-content {
        z-index: 2;
        opacity: 1;
        transition: all 0.35s;
      }
      @media only screen and (max-width: 500px) {
        .posts img {
          width: 10%;
          height: 1em;
        }

        .tabs {
          margin: 3em 0.5em;
        }
      }
    }
  }  
}
</style>
