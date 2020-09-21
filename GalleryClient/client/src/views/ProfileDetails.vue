<template>
  <div class="profile-details">
    <img v-if="state.loading" class="loader" src="@/assets/loading.gif" />
    <div v-else class="profile">
      <img v-if="state.profile.picture" :src="state.profile.picture" class="profile-icon" />
      <img v-else src="@/assets/icons/profile.png" class="profile-icon" />
      <router-link to="/#">Change profile picture</router-link>

      <div class="info">
        <p>Email: {{state.profile.email}}</p>
        <p>Username: {{state.profile.userName}}</p>
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
      loading: true
    });

    function isAuth() {
      return this.$store.state.auth.state.token !== "";
    }

    watchEffect(async () => {      
      const profile = await profileService.get();
      state.profile = profile;      
      state.loading = false;
    });

    return {
      state,
      isAuth
    };
  }
});
</script>

<style lang="scss">
.profile-details {
  .loader {
    margin: 0 auto;
    width: 10em;
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
      color: #A0A0A0;
      margin-bottom: 2em;
    }

    .info {
      
    }
  }
}
</style>
